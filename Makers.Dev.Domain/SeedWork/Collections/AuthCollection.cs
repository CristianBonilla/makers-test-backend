using Makers.Dev.Domain.SeedWork.Collections.Auth;

namespace Makers.Dev.Domain.SeedWork.Collections;

class AuthCollection
{
  public static UserCollection Users => new();
  public static RoleCollection Roles => new();
}
