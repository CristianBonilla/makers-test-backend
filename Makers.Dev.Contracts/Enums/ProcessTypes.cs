namespace Makers.Dev.Contracts.Enums;

public enum ProcessTypes : ushort
{
  Local = 0 | 1,
  IISExpress = 2,
  Docker = 3,
  DockerCompose = 4
}
