namespace Finisher.Web.Common;

internal static class ApiName
{
    public static readonly string Identity = nameof(Identity).ToLowerInvariant();
    public static readonly string Logger = nameof(Logger).ToLowerInvariant();
}

internal static class ApiPostFixName
{
    public static readonly string Popular = nameof(Popular).ToLowerInvariant();
    public static readonly string MostSell = nameof(MostSell).ToLowerInvariant();
    public static readonly string MostView = nameof(MostView).ToLowerInvariant();
}
