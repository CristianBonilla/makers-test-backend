using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Contracts.DTO.BankLoan;

public class BankLoanResponse
{
  public Guid BankLoanId { get; set; }
  public required Guid UserId { get; set; }
  public required BankLoanStatus Status { get; set; }
  public required decimal Amount { get; set; }
  public required DateTimeOffset PaymentTerm { get; set; }
  public DateTimeOffset Created { get; set; }
}
