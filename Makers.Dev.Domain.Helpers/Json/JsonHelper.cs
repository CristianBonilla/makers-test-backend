using System.Linq.Expressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Makers.Dev.Domain.Helpers.Json;

public class JsonHelper
{
  public static string ToJson<T>(T source, params Expression<Func<T, object>>[] omittedProperties) where T : class
  {
    string serialized = JsonConvert.SerializeObject(source, Formatting.None, new JsonSerializerSettings
    {
      ContractResolver = new JsonPropertiesOmissionResolver<T>(omittedProperties),
      DefaultValueHandling = DefaultValueHandling.Ignore,
      NullValueHandling = NullValueHandling.Ignore
    });

    return serialized;
  }

  public static T? ToObject<T>(string json, params Expression<Func<T, object>>[] omittedProperties) where T : class
  {
    T? deserialized = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
    {
      ContractResolver = new JsonPropertiesOmissionResolver<T>(omittedProperties),
      DefaultValueHandling = DefaultValueHandling.Ignore,
      NullValueHandling = NullValueHandling.Ignore
    });

    return deserialized;
  }

  public static JObject ToObject<T>(T source, params Expression<Func<T, object>>[] omittedProperties) where T : class
  {
    JObject jObject = JObject.FromObject(source, new()
    {
      ContractResolver = new JsonPropertiesOmissionResolver<T>(omittedProperties),
      DefaultValueHandling = DefaultValueHandling.Ignore,
      NullValueHandling = NullValueHandling.Ignore
    });

    return jObject;
  }
}
