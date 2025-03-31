using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Makers.Dev.API.Options;
using Makers.Dev.Domain.Helpers;

namespace Makers.Dev.API.Installers;

class JwtInstaller : IInstaller
{
  public void InstallServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
  {
    IConfigurationSection jwtSection = configuration.GetSection(nameof(JwtOptions));
    services.Configure<JwtOptions>(jwtSection);
    JwtOptions jwtOptions = jwtSection.Get<JwtOptions>()!;
    services.AddSingleton(jwtOptions);
    byte[] secretKey = JwtSigningKeyHelper.GetSecretKey(jwtOptions.Secret, 512);
    services.AddAuthentication(auth =>
    {
      auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
      jwt.RequireHttpsMetadata = !env.IsDevelopment(); // Development only
      jwt.SaveToken = true;
      jwt.TokenValidationParameters = new()
      {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        RequireExpirationTime = true
      };
    });
    services.AddAuthorization();
  }
}
