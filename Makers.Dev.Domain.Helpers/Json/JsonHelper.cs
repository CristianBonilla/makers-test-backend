using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Makers.Dev.Domain.Helpers.Json;

public class JsonHelper
{
  public static string ObjectToJson<T>(T source, params Expression<Func<T, object>>[] omittedProperties) where T : class
  {
    string json = JsonConvert.SerializeObject(source, Formatting.None, new JsonSerializerSettings
    {
      ContractResolver = new JsonPropertiesOmissionResolver<T>(omittedProperties),
      DefaultValueHandling = DefaultValueHandling.Ignore,
      NullValueHandling = NullValueHandling.Ignore
    });

    return json;
  }
}
