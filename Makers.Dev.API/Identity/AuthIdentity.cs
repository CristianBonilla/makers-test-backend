using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Newtonsoft.Json.Linq;
using Makers.Dev.API.Options;
using Makers.Dev.Contracts.DTO.Auth;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Contracts.Identity;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Domain.Helpers;
using Makers.Dev.Domain.Helpers.Exceptions;
using Makers.Dev.Domain.Helpers.Json;
using Makers.Dev.Contracts.DTO.Role;

namespace Makers.Dev.API.Identity;

class AuthIdentity(
  IMapper mapper,
  IAuthService authService,
  JwtOptions jwtOptions) : IAuthIdentity
{
  readonly IMapper _mapper = mapper;
  readonly IAuthService _authService = authService;
  readonly JwtOptions _jwtOptions = jwtOptions;

  public async Task<AuthResult> Register(UserRegisterRequest userRegisterRequest)
  {
    bool existingUser = await UserExists(userRegisterRequest);
    if (existingUser)
      throw AuthExceptionHelper.Unauthorized(userRegisterRequest);
    UserEntity user = _mapper.Map<UserEntity>(userRegisterRequest);
    UserEntity addedUser = await _authService.AddUser(user);
    RoleEntity role = await _authService.FindRoleById(user.RoleId);

    return GenerateAuthForUser(addedUser, role);
  }

  public async Task<AuthResult> Login(UserLoginRequest userLoginRequest)
  {
    UserEntity user = await _authService.FindUserByUsernameOrEmail(userLoginRequest.UsernameOrEmail);
    bool userValidPassoword = HashPasswordHelper.Verify(userLoginRequest.Password, user.Password, user.Salt);
    if (!userValidPassoword)
      throw AuthExceptionHelper.Unauthorized(userLoginRequest.Password);
    RoleEntity role = await _authService.FindRoleById(user.RoleId);

    return GenerateAuthForUser(user, role);
  }

  public async Task<bool> UserExists(UserRegisterRequest userRegisterRequest) => await _authService.UserExists(_mapper.Map<UserEntity>(userRegisterRequest));

  private AuthResult GenerateAuthForUser(UserEntity user, RoleEntity role)
  {
    JwtSecurityTokenHandler tokenHandler = new();
    byte[] secretKey = JwtSigningKeyHelper.GetSecretKey(_jwtOptions.Secret, 512);
    SecurityTokenDescriptor tokenDescriptor = new()
    {
      Subject = new([
        new(JwtRegisteredClaimNames.Sub, user.Email),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new(JwtRegisteredClaimNames.Email, user.Email),
        new(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
        new(ClaimTypes.NameIdentifier, user.Username),
        new(ClaimTypes.UserData, UserDataToJson(user, role)),
        new(ClaimTypes.Role, role.Name)
      ]),
      Expires = DateTime.UtcNow.AddDays(_jwtOptions.ExpiresInDays),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512Signature)
    };
    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

    return new(tokenHandler.WriteToken(token), _mapper.Map<UserResponse>(user), _mapper.Map<RoleResponse>(role));
  }

  private static string UserDataToJson(UserEntity user, RoleEntity role)
  {
    JObject jObject = JsonHelper.ToObject(new
    {
      user = JsonHelper.ToObject(
        user,
        user => user.UserId,
        user => user.Password,
        user => user.Salt,
        user => user.Version,
        user => user.Role,
        user => user.BankLoans),
      role = JsonHelper.ToObject(
        role,
        role => role.Version,
        role => role.Users)
    });

    return JsonHelper.ToJson(jObject);
  }
}
