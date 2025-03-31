namespace Makers.Dev.Contracts.DTO.BankLoan;

public class BankLoanRequest
{
  public required Guid UserId { get; set; }
  public required decimal Amount { get; set; }
  public required DateTimeOffset PaymentTerm { get; set; }
}
