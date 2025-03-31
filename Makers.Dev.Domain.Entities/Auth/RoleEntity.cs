namespace Makers.Dev.Domain.Entities.Auth;

public class RoleEntity
{
  public Guid RoleId { get; set; }
  public required string Name { get; set; }
  public required string DisplayName { get; set; }
  public DateTimeOffset Created { get; set; }
  public uint Version { get; set; }
  public ICollection<UserEntity> Users { get; set; } = [];
}
