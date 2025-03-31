using Makers.Dev.Domain.Helpers;

namespace Makers.Dev.API.Installers;

class AuthorizationInstaller : IInstaller
{
  public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    services.AddAuthorizationBuilder()
      .AddPolicy("AdminOnly", policy => policy.RequireRole(RolesHelper.AdminUser.Name));
  }
}
