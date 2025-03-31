using Makers.Dev.API.Utils;
using Makers.Dev.Contracts.Enums;
using Makers.Dev.Domain.Helpers;
using Makers.Dev.Infrastructure.Contexts.MakersDev;
using Microsoft.EntityFrameworkCore;

namespace Makers.Dev.API.Installers;

class DbInstaller : IInstaller
{
  public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    string connectionString = GetConnectionString(configuration);
    services.AddDbContextPool<MakersDevContext>(options => options.UseSqlServer(connectionString));
  }

  private static string GetConnectionString(IConfiguration configuration)
  {
    string connectionStringKey = ApiConfigKeys.GetConnectionKeyFromProcessType();
    string connectionString = configuration.GetConnectionString(connectionStringKey)
      ?? throw new InvalidOperationException($"Connection string \"{connectionStringKey}\" not established");
    if (ApiConfigKeys.ProcessType == ProcessTypes.Local)
      DirectoryConfigHelper.SetConnectionStringFullPathFromDataDirectory(ref connectionString);

    return connectionString;
  }
}
