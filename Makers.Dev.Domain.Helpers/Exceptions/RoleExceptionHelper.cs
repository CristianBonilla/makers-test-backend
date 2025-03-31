using System.Net;
using Makers.Dev.Contracts.Exceptions;

namespace Makers.Dev.Domain.Helpers.Exceptions;

public class RoleExceptionHelper
{
  public static ServiceErrorException NotFound(Guid roleId) => new(HttpStatusCode.NotFound, $"Role not found with role identifier \"{roleId}\"");
}
