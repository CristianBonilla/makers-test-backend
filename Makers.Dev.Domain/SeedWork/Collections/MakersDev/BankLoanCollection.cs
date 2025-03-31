using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Domain.Entities;
using Makers.Dev.Domain.SeedWork.Collections.Auth;

namespace Makers.Dev.Domain.SeedWork.Collections.MakersDev;

class BankLoanCollection : SeedDataCollection<BankLoanEntity>
{
  static readonly UserCollection _users = AuthCollection.Users;

  protected override BankLoanEntity[] Collection => [
    new()
    {
      BankLoanId = new("{c51322bd-4ae7-48dc-a1d4-55b8f3660f6b}"),
      UserId = _users[0].UserId,
      Status = BankLoanStatus.Pending,
      Amount = 70800000M,
      PaymentTerm = new(2025, 6, 22, 15, 20, 0, TimeSpan.FromHours(3))
    },
    new()
    {
      BankLoanId = new("{2a08545b-cf87-4501-ae6d-cc389ad3d6a0}"),
      UserId = _users[0].UserId,
      Status = BankLoanStatus.Approved,
      Amount = 1900000M,
      PaymentTerm = new(2025, 9, 19, 20, 11, 0, TimeSpan.FromHours(3))
    },
    new()
    {
      BankLoanId = new("{d4aca576-9a20-4ce4-b349-fc528fb76ce7}"),
      UserId = _users[0].UserId,
      Status = BankLoanStatus.Rejected,
      Amount = 120500000M,
      PaymentTerm = new(2027, 2, 12, 10, 0, 0, TimeSpan.FromHours(3))
    }
  ];
}
