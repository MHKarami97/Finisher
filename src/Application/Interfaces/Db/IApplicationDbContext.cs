namespace Finisher.Application.Interfaces.Db;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}
