namespace Makers.Dev.Domain.Entities.Auth;

public class RoleEntity
{
  public Guid RoleId { get; set; }
  public required string Name { get; set; }
  public required string DisplayName { get; set; }
  public DateTimeOffset Created { get; set; }
  public byte[] Version { get; set; } = null!;
  public ICollection<UserEntity> Users { get; set; } = [];
}
