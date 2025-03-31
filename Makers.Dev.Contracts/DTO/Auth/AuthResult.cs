using Makers.Dev.Contracts.DTO.Role;
using Makers.Dev.Contracts.DTO.User;

namespace Makers.Dev.Contracts.DTO.Auth;

public record AuthResult(string Token, UserResponse User, RoleResponse Role);
