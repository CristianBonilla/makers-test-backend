using System.Net;
using Makers.Dev.Contracts.Exceptions;

namespace Makers.Dev.Domain.Helpers.Exceptions;

public class UserExceptionHelper
{
  public static ServiceErrorException NotFound(Guid userId) => new(HttpStatusCode.NotFound, $"User not found with user identifier \"{userId}\"");
  public static ServiceErrorException NotFound(string usernameOrEmail) => new(HttpStatusCode.NotFound, $"User not found with user username or email \"{usernameOrEmail}\"");
  public static ServiceErrorException BadRequest(string roleName) => new(HttpStatusCode.BadRequest, $"User role named \"{roleName}\" cannot bank loans status change");
}
