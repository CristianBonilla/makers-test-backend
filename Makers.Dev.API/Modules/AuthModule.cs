using Autofac;
using Makers.Dev.Contracts.Identity;
using Makers.Dev.Contracts.Services;
using Makers.Dev.Domain.Services;
using Makers.Dev.Infrastructure.Repositories.Auth.Interfaces;
using Makers.Dev.Infrastructure.Repositories.Auth;
using Makers.Dev.API.Identity;

namespace Makers.Dev.API.Modules;

class AuthModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<RoleRepository>()
      .As<IRoleRepository>()
      .InstancePerLifetimeScope();
    builder.RegisterType<UserRepository>()
      .As<IUserRepository>()
      .InstancePerLifetimeScope();
    builder.RegisterType<AuthService>()
      .As<IAuthService>()
      .InstancePerLifetimeScope();
    builder.RegisterType<AuthIdentity>()
      .As<IAuthIdentity>()
      .InstancePerLifetimeScope();
  }
}
