using Makers.Dev.API.Mappers;

namespace Makers.Dev.API.Installers;

class MapperInstaller : IInstaller
{
  public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    services.AddAutoMapper(
      typeof(AuthProfile),
      typeof(MakersDevProfile));
  }
}
