using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.Contracts.Services;

public interface IAuthService
{
  Task<UserEntity> AddUser(UserEntity user);
  Task<UserEntity> UpdateUser(UserEntity user);
  Task<UserEntity> DeleteUserById(Guid userId);
  IAsyncEnumerable<(RoleEntity Role, IEnumerable<UserEntity> Users)> GetUsers();
  Task<UserEntity> FindUserById(Guid userId);
  Task<UserEntity> FindUserByUsernameOrEmail(string usernameOrEmail);
  Task<bool> UserExists(UserEntity user);
  Task<RoleEntity> FindRoleById(Guid roleId);
}
