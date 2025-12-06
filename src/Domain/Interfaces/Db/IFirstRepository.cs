namespace Finisher.Domain.Interfaces.Db;

public interface IFirstRepository<TEntity, in TPrimaryKey>
    where TEntity : class
{
    Task<TEntity> FirstAsTrackingAsync(TPrimaryKey id);
    Task<TResponse> FirstAsync<TResponse>(TPrimaryKey id);
    Task<TEntity> FirstAsync(TPrimaryKey id);
}
