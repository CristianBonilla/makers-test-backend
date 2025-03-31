using System.Net;
using Makers.Dev.Contracts.Exceptions;

namespace Makers.Dev.Domain.Helpers.Exceptions;

public class BankLoanExceptionHelper
{
  public static ServiceErrorException NotFound(Guid bankLoanId) => new(HttpStatusCode.NotFound, $"Bank loan not found with Bank loan identifier \"{bankLoanId}\"");
}
