using Makers.Dev.Contracts.DTO.User;

namespace Makers.Dev.Contracts.DTO.Role;

public class RoleResult
{
  public required RoleResponse Role { get; set; }
  public required IEnumerable<UserResponse?> Users { get; set; }
}
