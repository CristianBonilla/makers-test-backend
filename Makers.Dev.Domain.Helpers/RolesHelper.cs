using Makers.Dev.Contracts.DTO.Auth;

namespace Makers.Dev.Domain.Helpers;

public class RolesHelper
{
  public static Role AdminUser => new(new("{84b6844f-60a9-4336-a44d-4f23ba5fd12a}"), nameof(AdminUser), "Admin");
  public static Role CommonUser => new(new("{f9c078da-36f3-435f-9f52-666c659d2285}"), nameof(CommonUser), "Common User");

  public static bool IsAdminUserById(Guid roleId) => AdminUser.RoleId == roleId;

  public static bool IsCommonUserById(Guid roleId) => CommonUser.RoleId == roleId;
}
