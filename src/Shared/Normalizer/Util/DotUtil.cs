using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class DotUtil
{
    private static readonly Regex MatchConvertDotsToEllipsis =
        new(@"\s*\.{3,}", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// پاک کردن نقطه اضافه
    /// تبدیل متن.... به متن...
    /// </summary>
    public static string NormalizeDots(this string text) => MatchConvertDotsToEllipsis.Replace(text, @"…");
}
