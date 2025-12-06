namespace Finisher.Domain.Interfaces.Db;

public interface IRepository<TEntity> : IRepository<TEntity, int>
    where TEntity : class;

public interface IRepository<TEntity, in TPrimaryKey> :
    ISpecificationRepository<TEntity>,
    IFirstRepository<TEntity, TPrimaryKey>,
    IFirstOrDefaultRepository<TEntity, TPrimaryKey>,
    IDeleteRepository<TEntity, TPrimaryKey>,
    IGetListRepository<TEntity>,
    ICountRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}
