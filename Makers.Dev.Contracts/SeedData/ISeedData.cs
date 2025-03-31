namespace Makers.Dev.Contracts.SeedData;

public interface ISeedData
{
  SeedAuthData Auth { get; }
  SeedMakersDevData Makers { get; }
}
