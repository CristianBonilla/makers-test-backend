using Makers.Dev.Contracts.DTO.Auth;
using Makers.Dev.Contracts.DTO.User;

namespace Makers.Dev.Contracts.Identity;

public interface IAuthIdentity
{
  Task<AuthResult> Register(UserRegisterRequest userRegisterRequest);
  Task<AuthResult> Login(UserLoginRequest userLoginRequest);
  Task<bool> UserExists(UserRegisterRequest user);
}
