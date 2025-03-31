using Makers.Dev.Contracts.DTO.Role;

namespace Makers.Dev.Contracts.DTO.User;

public class UsersResult
{
  public required RoleResponse Role { get; set; }
  public required IEnumerable<UserResponse> Users { get; set; }
}
