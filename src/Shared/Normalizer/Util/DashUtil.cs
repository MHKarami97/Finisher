using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class DashUtil
{
    private static readonly Regex MatchFixDashes1 =
        new(@"-{3}", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchFixDashes2 =
        new(@"-{2}", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// پاک کردن دش های اضافه بین کلمات
    /// تبدیل متن---من--این به متن-من-این
    /// </summary>
    public static string NormalizeDashes(this string text)
    {
        var phase1 = MatchFixDashes1.Replace(text, "—");
        var phase2 = MatchFixDashes2.Replace(phase1, "–");
        return phase2;
    }
}
