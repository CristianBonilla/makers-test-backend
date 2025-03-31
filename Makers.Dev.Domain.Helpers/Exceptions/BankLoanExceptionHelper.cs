using System.Net;
using Makers.Dev.Contracts.Exceptions;
using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Domain.Helpers.Exceptions;

public class BankLoanExceptionHelper
{
  public static ServiceErrorException NotFound(Guid bankLoanId) => new(HttpStatusCode.NotFound, $"Bank loan not found with Bank loan identifier \"{bankLoanId}\"");
  public static ServiceErrorException NotFound(Guid userId, Guid bankLoanId) => new(HttpStatusCode.NotFound, $"Bank loan not found with user identifier \"{userId}\" and bank loan identifier \"{bankLoanId}\"");
  public static ServiceErrorException BadRequest(BankLoanStatus from, BankLoanStatus to) => new(HttpStatusCode.BadRequest, $"Bank loan status from \"{Enum.GetName(from)}\" to \"{Enum.GetName(to)}\" can no longer be changed");
  public static ServiceErrorException Forbidden(Exception exception) => new(HttpStatusCode.Forbidden, "An internal database error occurred and the loan status could not be changed", exception.Message);
}
