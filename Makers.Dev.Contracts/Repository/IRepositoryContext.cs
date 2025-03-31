using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Makers.Dev.Contracts.Repository;
public interface IRepositoryContext<in TContext> : IDisposable where TContext : DbContext
{
  DbSet<TEntity> Set<TEntity>() where TEntity : class;
  EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
  int Save();
  Task<int> SaveAsync();
}
