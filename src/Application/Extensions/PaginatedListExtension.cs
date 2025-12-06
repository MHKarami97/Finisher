namespace Finisher.Application.Extensions;

public static class PaginatedListExtension<T>
{
    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, pageNumber);
    }
}

public static class PaginatedListWithCountExtension<T>
{
    public static async Task<PaginatedListWithCount<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedListWithCount<T>(items, count, pageNumber, pageSize);
    }
}
