using System.Linq.Expressions;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Domain.Helpers;
using Makers.Dev.Domain.Helpers.Exceptions;
using Makers.Dev.Infrastructure.Repositories.Auth.Interfaces;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.Domain.Services;

public class AuthService(
  IMakersDevRepositoryContext context,
  IUserRepository userRepository,
  IRoleRepository roleRepository) : IAuthService
{
  readonly IMakersDevRepositoryContext _context = context;
  readonly IUserRepository _userRepository = userRepository;
  readonly IRoleRepository _roleRepository = roleRepository;

  public async Task<UserEntity> AddUser(UserEntity user)
  {
    CheckRoleById(user.RoleId);
    UpdateUserPassword(ref user);
    UserEntity addedUser = _userRepository.Create(user);
    _ = await _context.SaveAsync();

    return addedUser;
  }

  public async Task<UserEntity> UpdateUser(UserEntity user)
  {
    CheckRoleById(user.RoleId);
    CheckUserById(user.UserId);
    bool existingUser = UserExists(user, source => source.UserId != user.UserId);
    if (existingUser)
      throw AuthExceptionHelper.Unauthorized(user);
    UpdateUserPassword(ref user);
    UserEntity updatedUser = _userRepository.Update(user);
    _ = await _context.SaveAsync();

    return updatedUser;
  }

  public async Task<UserEntity> DeleteUserById(Guid userId)
  {
    UserEntity user = GetUserById(userId);
    UserEntity deletedUser = _userRepository.Delete(user);
    _ = await _context.SaveAsync();

    return deletedUser;
  }

  public IAsyncEnumerable<(RoleEntity Role, IEnumerable<UserEntity> Users)> GetUsers(Guid superUserId)
  {
    CheckUserById(superUserId);
    CheckUserPermission(superUserId);
    var users = _roleRepository
      .GetAll()
      .GroupJoin(
        _userRepository
          .GetAll(users => users
            .OrderBy(order => order.Username)
            .ThenBy(order => order.Firstname)
            .ThenBy(order => order.Lastname)),
        role => role.RoleId,
        user => user.RoleId,
        (role, users) => (role, users))
      .ToAsyncEnumerable();

    return users;
  }

  public Task<UserEntity> FindUserById(Guid userId) => Task.FromResult(GetUserById(userId));

  public Task<UserEntity> FindUserByUsernameOrEmail(string usernameOrEmail)
  {
    UserEntity user = _userRepository.GetAll()
      .FirstOrDefault(user => StringCommonHelper.IsEquivalent(user.Username, usernameOrEmail) || StringCommonHelper.IsEquivalent(user.Email, usernameOrEmail))
      ?? throw UserExceptionHelper.NotFound(usernameOrEmail);

    return Task.FromResult(user);
  }

  public Task<bool> UserExists(UserEntity user) => Task.FromResult(UserExists(user, null));

  private static void UpdateUserPassword(ref UserEntity user)
  {
    var (password, salt) = HashPasswordHelper.Create(user.Password);
    user.Password = password;
    user.Salt = salt;
    user.IsActive = true;
  }

  private void CheckRoleById(Guid roleId)
  {
    bool existingRole = _roleRepository.Exists(role => role.RoleId == roleId);
    if (!existingRole)
      throw RoleExceptionHelper.NotFound(roleId);
  }

  private void CheckUserById(Guid userId)
  {
    bool existingUser = _userRepository.Exists(user => user.UserId == userId);
    if (!existingUser)
      throw UserExceptionHelper.NotFound(userId);
  }

  private void CheckUserPermission(Guid userId)
  {
    UserEntity user = _userRepository.Find([userId]) ?? throw UserExceptionHelper.NotFound(userId);
    RoleEntity role = _roleRepository.Find([user.RoleId]) ?? throw RoleExceptionHelper.NotFound(user.RoleId);
    if (!RolesHelper.IsAdminUserById(role.RoleId))
      throw UserExceptionHelper.BadRequest(role.Name);
  }

  private UserEntity GetUserById(Guid userId)
  {
    UserEntity user = _userRepository.Find([userId]) ?? throw UserExceptionHelper.NotFound(userId);

    return user;
  }

  private bool UserExists(UserEntity user, Expression<Func<UserEntity, bool>>? filter)
  {
    bool DocumentNumberExists(string documentNumber) => StringCommonHelper.IsEquivalent(user.DocumentNumber, documentNumber);
    bool MobileExists(string mobile) => StringCommonHelper.IsEquivalent(user.Mobile, mobile);
    bool UsernameExists(string username) => StringCommonHelper.IsEquivalent(user.Username, username);
    bool EmailExists(string email) => StringCommonHelper.IsEquivalent(user.Email, email);
    var users = filter is not null ? _userRepository.GetByFilter(filter) : _userRepository.GetAll();
    
    return users.Any(source => DocumentNumberExists(source.DocumentNumber) || MobileExists(source.Mobile) || UsernameExists(source.Username) || EmailExists(source.Email));
  }
}
