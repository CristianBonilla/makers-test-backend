using Autofac;
using Makers.Dev.Contracts.Repository;
using Makers.Dev.Infrastructure.Repositories;
using Makers.Dev.Infrastructure.Repositories.MakersDev;
using Makers.Dev.Infrastructure.Repositories.MakersDev.Interfaces;

namespace Makers.Dev.API.Modules;

class GlobalRepositoriesModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterGeneric(typeof(RepositoryContext<>))
      .As(typeof(IRepositoryContext<>))
      .InstancePerLifetimeScope();
    builder.RegisterGeneric(typeof(Repository<,>))
      .As(typeof(IRepository<,>))
      .InstancePerLifetimeScope();
    builder.RegisterType<MakersDevRepositoryContext>()
      .As<IMakersDevRepositoryContext>()
      .InstancePerLifetimeScope();
  }
}
