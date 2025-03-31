using Makers.Dev.Domain.Entities.Auth;

namespace Makers.Dev.Domain.Entities;

public class BankLoanEntity
{
  public Guid BankLoanId { get; set; }
  public required Guid UserId { get; set; }
  public required BankLoanStatus Status { get; set; }
  public required decimal Amount { get; set; }
  public required DateTimeOffset PaymentTerm { get; set; }
  public DateTimeOffset Created { get; set; }
  public uint Version { get; set; }
  public UserEntity User { get; set; } = null!;
}
