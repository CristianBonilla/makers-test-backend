using Makers.Dev.Contracts.DTO.Role;
using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Domain.Entities.Auth;
using Makers.Dev.Domain.Helpers;

namespace Makers.Dev.Domain.SeedWork.Collections.Auth;

class RoleCollection : SeedDataCollection<RoleEntity>
{
  readonly Role AdminUser = RolesHelper.AdminUser;
  readonly Role CommonUser = RolesHelper.CommonUser;

  protected override RoleEntity[] Collection => [
    new()
    {
      RoleId = AdminUser.RoleId,
      Name = AdminUser.Name,
      DisplayName = AdminUser.DisplayName
    },
    new()
    {
      RoleId = CommonUser.RoleId,
      Name = CommonUser.Name,
      DisplayName = CommonUser.DisplayName
    }
  ];
}
