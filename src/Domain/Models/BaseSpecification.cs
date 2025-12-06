namespace Finisher.Domain.Models;

public abstract class BaseSpecification<T> : Specification<T>
    where T : class
{
    protected BaseSpecification()
    {
        Query.TagWith(GetType().Name);
    }
}

public abstract class CachedSpecification<T> : BaseSpecification<T>
    where T : class
{
    protected CachedSpecification(string cacheKey, object cacheParameter)
    {
        Query.EnableCache(cacheKey, cacheParameter);
    }
}

public abstract class NotTrackSpecification<T> : BaseSpecification<T>
    where T : class
{
    protected NotTrackSpecification()
    {
        Query.AsNoTracking();
    }
}

public abstract class NotTrackAsCachedSpecification<T> : CachedSpecification<T>
    where T : class
{
    protected NotTrackAsCachedSpecification(string cacheKey, object cacheParameter)
        : base(cacheKey, cacheParameter)
    {
        Query.AsNoTracking();
    }
}
