namespace Finisher.Domain.Interfaces.Db;

public interface IDeleteRepository<TEntity, in TPrimaryKey>
    where TEntity : class
{
    Task DeleteAsync(TEntity entity);
    Task DeleteAsync(TPrimaryKey id);
}
