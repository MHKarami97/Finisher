namespace Finisher.Domain.Interfaces.Db;

public interface IGetListRepository<TEntity>
    where TEntity : class
{
    Task<PaginatedList<TResponse>> GetListPaginateAsync<TResponse>(PaginationQuery pagination) where TResponse : class;
    Task<PaginatedList<TResponse>> GetListPaginateDescAsync<TResponse>(PaginationQuery pagination) where TResponse : class;
}
