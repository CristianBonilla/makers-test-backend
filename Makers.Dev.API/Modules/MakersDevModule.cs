using Autofac;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Services;
using Makers.Dev.Infrastructure.Repositories.MakersDev;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.API.Modules;

class MakersDevModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<BankLoanRepository>()
      .As<IBankLoanRepository>()
      .InstancePerLifetimeScope();
    builder.RegisterType<BankLoanService>()
      .As<IBankLoanService>()
      .InstancePerLifetimeScope();
  }
}
