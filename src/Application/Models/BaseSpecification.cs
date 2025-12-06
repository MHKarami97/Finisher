namespace Finisher.Application.Models;

public abstract class BaseSpecification<T, TOut> : Specification<T, TOut>
    where T : class
{
    protected BaseSpecification()
    {
        Query.TagWith(GetType().Name);
    }
}

public abstract class CachedSpecification<T, TOut> : BaseSpecification<T, TOut>
    where T : class
{
    protected CachedSpecification(string cacheKey, object cacheParameter)
    {
        Query.EnableCache(cacheKey, cacheParameter);
    }
}

public abstract class NotTrackSpecification<T, TOut> : BaseSpecification<T, TOut>
    where T : class
{
    protected NotTrackSpecification()
    {
        Query.AsNoTracking();
    }
}

public abstract class NotTrackAsCachedSpecification<T, TOut> : CachedSpecification<T, TOut>
    where T : class
{
    protected NotTrackAsCachedSpecification(string cacheKey, object cacheParameter)
        : base(cacheKey, cacheParameter)
    {
        Query.AsNoTracking();
    }
}
