using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class HexadecimalSymbolsUtil
{
    private static readonly Regex MatchHexadecimalSymbols = new("[\x00-\x08\x0B\x0C\x0E-\x1F]",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// پاک کردن کاراکترهای غیر قابل چاپ و نمایش
    /// </summary>
    public static string RemoveHexadecimalSymbols(this string? txt)
        => string.IsNullOrWhiteSpace(txt) ? string.Empty : MatchHexadecimalSymbols.Replace(txt, string.Empty);
}
