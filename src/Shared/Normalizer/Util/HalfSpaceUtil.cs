using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class HalfSpaceUtil
{
    private static readonly Regex MatchApplyHalfSpaceRule1 =
        new(@"\s+(ن?می)\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchApplyHalfSpaceRule2 =
        new(@"\s+(تر(ی(ن)?)?|ها(ی)?)\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchCleanupZwnj =
        new(@"\s+‌|‌\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchYeHeHalfSpace =
        new(@"(\S)(ه[\s‌]+[یی])(\s)", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// درست کردن نیم فاصله
    /// </summary>
    public static string ApplyHalfSpaceRule(this string text)
    {
        text = text.NormalizeZwnj();

        //put zwnj between word and prefix (mi* nemi*)
        var phase1 = MatchApplyHalfSpaceRule1.Replace(text, " $1‌");

        //put zwnj between word and suffix (*tar *tarin *ha *haye)
        var phase2 = MatchApplyHalfSpaceRule2.Replace(phase1, "‌$1 ");

        var phase3 = phase2.NormalizeYeHeHalfSpace();
        return phase3;
    }

    /// <summary>
    /// پاک کردن zwnj اضافه
    /// </summary>
    private static string NormalizeZwnj(this string text) => MatchCleanupZwnj.Replace(text, " ");

    /// <summary>
    /// Converts ه ی to ه‌ی
    /// </summary>
    private static string NormalizeYeHeHalfSpace(this string text) =>
        MatchYeHeHalfSpace.Replace(text, "$1ه‌ی‌$3");
}
