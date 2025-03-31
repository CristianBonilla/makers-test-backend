using System.Reflection;
using Makers.Dev.Contracts.Enums;
using Makers.Dev.Domain.Helpers.Extensions;

namespace Makers.Dev.Domain.Helpers;

public class DirectoryConfigHelper
{
  public const string DATA_DIRECTORY = "[DataDirectory]";
  public const string APPDATA_DIRECTORY = "App_Data";

  public static string DirectoryPath
  {
    get
    {
      string baseDirectory = Directory.GetCurrentDirectory();
      int lastIndex = baseDirectory
        .ToLower()
        .LastIndexOf(@"bin\debug\");

      return lastIndex switch
      {
        >= 0 => baseDirectory[..lastIndex],
        _ => baseDirectory
      };
    }
  }

  public static string GetDirectoryFilePathFromAssemblyName(FileFormatTypes fileFormatType, Assembly? assembly = null)
  {
    assembly ??= Assembly.GetExecutingAssembly();
    AssemblyName assemblyName = assembly.GetName();
    string path = Path.Combine(DirectoryPath, $"{assemblyName.Name!}{fileFormatType.GetFormatType()}");

    return path;
  }

  public static void SetConnectionStringFullPathFromDataDirectory(ref string connectionString, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
  {
    if (string.IsNullOrEmpty(connectionString))
      throw new ArgumentNullException(nameof(connectionString), "The connection string should be defined");
    string appDataPath = Path.Combine(DirectoryPath, APPDATA_DIRECTORY);
    if (!Directory.Exists(appDataPath))
      Directory.CreateDirectory(appDataPath);
    connectionString = connectionString.Replace(
      DATA_DIRECTORY,
      appDataPath,
      comparison);
  }
}
