namespace Makers.Dev.Domain.Helpers.Extensions;

static class EvaluateTypesExtensions
{
  const string MAKERS_DEV = "Makers.Dev";

  public static bool IsUserDefinedObject(this Type propertyType)
  {
    string? propertyAssemblyName = propertyType.Assembly.FullName;
    bool isClass = propertyType.IsClass;
    bool isStruct = propertyType.IsValueType && !propertyType.IsPrimitive && !propertyType.IsEnum;

    return (isClass || isStruct) && (propertyAssemblyName?.StartsWith(MAKERS_DEV) ?? false);
  }
}
