using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.Contracts.SeedData;

public class SeedAuthData
{
  public required SeedDataCollection<RoleEntity> Roles { get; set; }
  public required SeedDataCollection<UserEntity> Users { get; set; }
}
