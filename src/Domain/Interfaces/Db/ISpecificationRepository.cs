namespace Finisher.Domain.Interfaces.Db;

public interface ISpecificationRepository<TEntity>
{
    Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification);
    Task<TEntity> FirstAsync(ISpecification<TEntity> specification);
    Task<TResponse> FirstAsync<TResponse>(ISpecification<TEntity, TResponse> specification);
    Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification);
    Task<int> CountAsync(ISpecification<TEntity> specification);
    Task<bool> AnyAsync(ISpecification<TEntity> specification);
}
