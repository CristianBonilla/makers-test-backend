namespace Makers.Dev.Contracts.DTO.Role;

public class RoleResponse
{
  public Guid RoleId { get; set; }
  public required string Name { get; set; }
  public required string DisplayName { get; set; }
  public DateTimeOffset Created { get; set; }
}
