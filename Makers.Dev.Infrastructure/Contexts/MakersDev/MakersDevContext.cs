using Microsoft.EntityFrameworkCore;
using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Infrastructure.Contexts.MakersDev.Config;
using Makers.Dev.Infrastructure.Extensions;

namespace Makers.Dev.Infrastructure.Contexts.MakersDev;

public class MakersDevContext(DbContextOptions<MakersDevContext> options, ISeedData? seedData) : DbContext(options)
{
  protected override void OnModelCreating(ModelBuilder builder)
  {
    builder.ApplyEntityTypeConfig(seedData,
      typeof(RoleConfig),
      typeof(UserConfig));
    builder.ApplyEntityTypeConfig(seedData,
      typeof(BankLoanConfig));
  }
}
