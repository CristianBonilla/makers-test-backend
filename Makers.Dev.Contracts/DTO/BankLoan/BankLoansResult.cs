using Makers.Dev.Contracts.DTO.User;

namespace Makers.Dev.Contracts.DTO.BankLoan;

public class BankLoansResult
{
  public required UserResponse User { get; set; }
  public required IEnumerable<BankLoanResponse> BankLoans { get; set; }
}
