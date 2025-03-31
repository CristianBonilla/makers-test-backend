using Makers.Dev.Domain.Entities;

namespace Makers.Dev.Contracts.SeedData;

public class SeedMakersDevData
{
  public required SeedDataCollection<BankLoanEntity> BankLoans { get; set; }
}
