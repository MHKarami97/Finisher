namespace Finisher.Web.Common;

public static class ApiRoutes
{
    public const string Api = "api";
    public const string VersioningPrefix = "v";
    public const string Separator = "/";
    public const string IdRoute = "{id:int}";
    public const string IdRouteString = "{id}";
    public const string Pagination = "pagination";
    public const string Id = "id";

    public static string AddSeparator(this string str)
    {
        return str.EndsWith(Separator, StringComparison.Ordinal) ? str : str + Separator;
    }

    public static string AddPagination(this string str)
    {
        return str.EndsWith(Separator, StringComparison.Ordinal) ? str + Pagination : str.AddSeparator() + Pagination;
    }

    public static string AddPostFix(this string str, string postfix)
    {
        return str.EndsWith(Separator, StringComparison.Ordinal) ? str + postfix.AddSeparator() + Pagination : str.AddSeparator() + postfix.AddSeparator() + Pagination;
    }
}
