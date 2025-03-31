using System.Linq.Expressions;
using System.Reflection;

namespace Makers.Dev.Domain.Helpers.Extensions;

static class PropertyFromExpressionExtensions
{
  public static PropertyInfo? GetProperty(this LambdaExpression expression)
    => expression.Body switch
    {
      MemberExpression bodys => (PropertyInfo)bodys.Member,
      UnaryExpression unary when unary.Operand is MemberExpression operand => (PropertyInfo)operand.Member,
      _ => null
    };

  public static bool IsIncludedProperty(this IEnumerable<LambdaExpression> expressions, PropertyInfo property)
    => expressions.Any(expression => property.Equals(expression.GetProperty()));
}
