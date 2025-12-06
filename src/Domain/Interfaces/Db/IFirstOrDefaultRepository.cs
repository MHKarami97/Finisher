namespace Finisher.Domain.Interfaces.Db;

public interface IFirstOrDefaultRepository<TEntity, in TPrimaryKey>
    where TEntity : class
{
    Task<TResponse?> FirstOrDefaultAsync<TResponse>(TPrimaryKey id);
}
