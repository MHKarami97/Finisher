using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class ExtraMarksUtil
{
    private static readonly Regex MatchCleanupExtraMarks1 =
        new(@"(!){2,}", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchCleanupExtraMarks2 =
        new("(؟){2,}", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// پاک کردن ؟ ! اضافی
    /// </summary>
    public static string RemoveExtraMarks(this string text)
    {
        var phase1 = MatchCleanupExtraMarks1.Replace(text, "$1");
        var phase2 = MatchCleanupExtraMarks2.Replace(phase1, "$1");

        return phase2;
    }
}
