using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Makers.Dev.Domain.Helpers.Extensions;

namespace Makers.Dev.Domain.Helpers.Json;

public class JsonPropertiesOmissionResolver<TObject>(params Expression<Func<TObject, object>>[] omittedProperties) : CamelCasePropertyNamesContractResolver where TObject : class
{
  readonly Expression<Func<TObject, object>>[] _omittedProperties = omittedProperties;

  public IContractResolver Instance => new JsonPropertiesOmissionResolver<TObject>(_omittedProperties);

  protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
  {
    JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
    PropertyInfo property = (PropertyInfo)member;
    if (_omittedProperties.IsIncludedProperty(property))
    {
      jsonProperty.ShouldSerialize = _ => false;
      jsonProperty.ShouldDeserialize = _ => false;
    }

    return jsonProperty;
  }
}
