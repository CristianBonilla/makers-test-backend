using Autofac;
using Makers.Dev.Contracts.SeedData;
using Makers.Dev.Domain.SeedWork;

namespace Makers.Dev.API.Modules;

class DbModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<SeedData>()
      .As<ISeedData>()
      .InstancePerLifetimeScope();
  }
}
