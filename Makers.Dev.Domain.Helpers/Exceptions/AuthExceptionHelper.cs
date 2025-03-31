using System.Net;
using Makers.Dev.Contracts.DTO.User;
using Makers.Dev.Contracts.Exceptions;
using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.Domain.Helpers.Exceptions;

public class AuthExceptionHelper
{
  public static ServiceErrorException Unauthorized(UserEntity user) => Unauthorized(user.DocumentNumber, user.Mobile, user.Username, user.Email);
  public static ServiceErrorException Unauthorized(UserRegisterRequest user) => Unauthorized(user.DocumentNumber, user.Mobile, user.Username, user.Email);
  public static ServiceErrorException Unauthorized(string password) => new(HttpStatusCode.Unauthorized, $"User password is invalid \"{password}\"");

  private static ServiceErrorException Unauthorized(string documentNumber, string mobile, string username, string email)
    => new(HttpStatusCode.Unauthorized, $"The user already exists. Please verify info provided: documentNumber \"{documentNumber}\", mobile \"{mobile}\", username \"{username}\", email \"{email}\"");
}
