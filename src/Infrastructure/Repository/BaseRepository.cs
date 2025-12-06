using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Mapster;
using Finisher.Application.Interfaces.Db;
using Finisher.Application.Mappings;
using Finisher.Domain.Interfaces.Db;
using Finisher.Domain.Models;

namespace Finisher.Infrastructure.Repository;

public class BaseRepository<TEntity>(IApplicationDbContext context) : BaseRepository<TEntity, int>(context) where TEntity : class;

public class BaseRepository<TEntity, TPrimaryKey>(IApplicationDbContext context) : IRepository<TEntity, TPrimaryKey>
    where TEntity : class
{
    protected DbSet<TEntity> DbSet { get; } = context.Set<TEntity>();

    #region Main

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
        return entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        return Task.FromResult(entity);
    }

    #endregion

    #region Delete

    public virtual Task DeleteAsync(TEntity entity)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task DeleteAsync(TPrimaryKey id)
    {
        var entity = await FirstAsTrackingAsync(id);

        await DeleteAsync(entity);
    }

    #endregion

    #region First

    public virtual async Task<TEntity> FirstAsTrackingAsync(TPrimaryKey id)
    {
        return await DbSet.AsTracking().FirstAsync(x => EF.Property<TPrimaryKey>(x, "Id")!.Equals(id));
    }

    public virtual async Task<TEntity> FirstAsync(TPrimaryKey id)
    {
        return await DbSet
            .FirstAsync(x => EF.Property<TPrimaryKey>(x, "Id")!.Equals(id));
    }

    public virtual async Task<TResponse> FirstAsync<TResponse>(TPrimaryKey id)
    {
        return await DbSet
            .ProjectToType<TResponse>()
            .FirstAsync(x => EF.Property<TPrimaryKey>(x!, "Id")!.Equals(id));
    }

    public virtual async Task<TResponse?> FirstOrDefaultAsync<TResponse>(TPrimaryKey id)
    {
        return await DbSet
            .ProjectToType<TResponse>()
            .FirstOrDefaultAsync(x => EF.Property<TPrimaryKey>(x!, "Id")!.Equals(id));
    }

    #endregion

    #region Paginate

    public virtual Task<PaginatedList<TResponse>> GetListPaginateAsync<TResponse>(PaginationQuery pagination)
        where TResponse : class
    {
        return DbSet
            .ProjectToType<TResponse>()
            .PaginatedListAsync(pagination.PageNumber, pagination.PageSize);
    }

    public virtual Task<PaginatedList<TResponse>> GetListPaginateDescAsync<TResponse>(PaginationQuery pagination)
        where TResponse : class
    {
        return DbSet
            .OrderByDescending(a => EF.Property<object>(a, "Id"))
            .ProjectToType<TResponse>()
            .PaginatedListAsync(pagination.PageNumber, pagination.PageSize);
    }

    #endregion

    #region Count

    public virtual Task<int> CountAsync()
    {
        return DbSet.CountAsync();
    }

    public virtual Task<long> LongCountAsync()
    {
        return DbSet.LongCountAsync();
    }

    #endregion

    #region Specification

    public async Task<TEntity?> FirstOrDefaultAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity> FirstAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);

        return await query.FirstAsync();
    }

    public async Task<TResponse> FirstAsync<TResponse>(ISpecification<TEntity, TResponse> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);

        return await query.FirstAsync();
    }

    public async Task<List<TEntity>> GetListAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);

        return await query.ToListAsync();
    }

    public async Task<int> CountAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);

        return await query.CountAsync();
    }

    public async Task<bool> AnyAsync(ISpecification<TEntity> specification)
    {
        var query = SpecificationEvaluator.Default.GetQuery(DbSet.AsQueryable(), specification);

        return await query.AnyAsync();
    }

    #endregion
}
