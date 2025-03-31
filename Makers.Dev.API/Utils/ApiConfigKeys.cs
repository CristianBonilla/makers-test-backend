using Makers.Dev.Contracts.Enums;

namespace Makers.Dev.API.Utils;

record struct ApiConfigKeys
{
  public const string AllowOrigins = nameof(AllowOrigins);
  public const string Bearer = nameof(Bearer);
  public const string LocalConnection = nameof(LocalConnection);
  public const string DockerConnection = nameof(DockerConnection);
  public const string DockerComposeConnection = nameof(DockerComposeConnection);

  static readonly Dictionary<ProcessTypes, string> _processTypes = new()
  {
    { ProcessTypes.Local, LocalConnection },
    { ProcessTypes.IISExpress, LocalConnection },
    { ProcessTypes.Docker, DockerConnection },
    { ProcessTypes.DockerCompose, DockerComposeConnection }
  };

  public static ProcessTypes ProcessType
  {
    get
    {
      string processTypeValue = Environment.GetEnvironmentVariable("PROCESS_TYPE") switch
      {
        null => nameof(ProcessTypes.Local),
        string source when string.Equals(source, "iis-express", StringComparison.OrdinalIgnoreCase) => nameof(ProcessTypes.IISExpress),
        string source when string.Equals(source, "docker-compose", StringComparison.OrdinalIgnoreCase) => nameof(ProcessTypes.DockerCompose),
        string source => source
      };

      return Enum.TryParse(processTypeValue, true, out ProcessTypes processType) ? processType : ProcessTypes.Local;
    }
  }

  public static string GetConnectionKeyFromProcessType() => _processTypes.GetValueOrDefault(ProcessType) ?? throw new KeyNotFoundException("Process type not found to get connection key");
}
