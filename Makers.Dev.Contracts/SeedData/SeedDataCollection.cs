using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Makers.Dev.Contracts.SeedData;

public abstract class SeedDataCollection<TData> where TData : class
{
  public int Count => Collection.Length;

  protected abstract TData[] Collection { get; }

  public TData this[int index] => Collection[index];

  public IEnumerable<TData> GetAll() => [.. Collection];

  public TProperty GetFromProperty<TProperty>(int index, Expression<Func<TData, TProperty>> property)
  {
    TData data = Collection[index];
    object? propertyValue = property.GetPropertyAccess().GetValue(data, null);

    return (propertyValue is TProperty value ? value : default)!;
  }
}
