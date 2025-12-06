namespace Finisher.Shared.Consts;

public static class GlobalConsts
{
    public const int MinUrlLength = 3;
    public const int MaxUrlLength = 100;
    public const string UrlRegex = @"^[a-z]+(-[a-z]+)*$";

    public const int MinTitleLength = 3;
    public const int MaxTitleLength = 400;
}
