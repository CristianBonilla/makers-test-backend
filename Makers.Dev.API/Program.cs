using Autofac.Extensions.DependencyInjection;
using Makers.Dev.API.Utils;
using Makers.Dev.Contracts.Enums;
using Makers.Dev.Infrastructure.Contexts.MakersDev;

namespace Makers.Dev.API;

public class Program
{
  public static async Task Main(string[] args)
  {
    IHost host = CreateHostBuilder(args).Build();
    await DbConnectionSingleton.Start(host).Connect<MakersDevContext>(DbConnectionTypes.Migration);
    await host.RunAsync();
  }

  private static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
      .UseServiceProviderFactory(new AutofacServiceProviderFactory())
      .ConfigureWebHostDefaults(builder =>
      {
        builder.UseStartup<Startup>();
      });
}
