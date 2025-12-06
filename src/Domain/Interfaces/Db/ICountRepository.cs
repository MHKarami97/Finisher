namespace Finisher.Domain.Interfaces.Db;

public interface ICountRepository<TEntity>
    where TEntity : class
{
    Task<int> CountAsync();
    Task<long> LongCountAsync();
}
