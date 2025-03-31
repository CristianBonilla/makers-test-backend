using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Domain.SeedWork.Collections;

namespace Makers.Dev.Domain.SeedWork;

public class SeedData : ISeedData
{
  public SeedAuthData Auth => new()
  {
    Users = AuthCollection.Users,
    Roles = AuthCollection.Roles
  };

  public SeedMakersDevData Makers => new()
  {
    BankLoans = MakersDevCollection.BankLoans
  };
}
