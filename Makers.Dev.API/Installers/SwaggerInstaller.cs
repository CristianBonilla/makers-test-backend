using System.Reflection;
using Microsoft.OpenApi.Models;
using Makers.Dev.API.Filters;
using Makers.Dev.API.Utils;
using Makers.Dev.Contracts.Enums;
using Makers.Dev.Domain.Helpers;
using Makers.Dev.API.Options;

namespace Makers.Dev.API.Installers;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
class SwaggerInstaller : IInstaller
{
  public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    IConfigurationSection swaggerSection = configuration.GetSection(nameof(SwaggerOptions));
    services.Configure<SwaggerOptions>(swaggerSection);
    SwaggerOptions swagger = swaggerSection.Get<SwaggerOptions>()!;
    OpenApiInfo info = swagger.Info;
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc(info.Version, info);
      options.SchemaFilter<EnumSchemaFilter>();
      if (swagger?.SecurityScheme is not null)
      {
        OpenApiSecurityScheme apiSecurity = swagger.SecurityScheme;
        apiSecurity.Reference = new()
        {
          Id = ApiConfigKeys.Bearer,
          Type = ReferenceType.SecurityScheme
        };
        options.AddSecurityDefinition(ApiConfigKeys.Bearer, apiSecurity);
        options.AddSecurityRequirement(new() { { apiSecurity, new List<string>() } });
      }
      string xmlCommentsFilePath = DirectoryConfigHelper.GetDirectoryFilePathFromAssemblyName(FileFormatTypes.Xml, Assembly.GetExecutingAssembly());
      options.IncludeXmlComments(xmlCommentsFilePath, true);
    });
  }
}
