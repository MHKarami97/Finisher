using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class SpacingAndLineBreaksUtil
{
    private static readonly Regex MatchCleanupSpacingAndLineBreaks1 =
        new(@"[ ]+", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchCleanupSpacingAndLineBreaks2 = new("([\n]+)[   ‌]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// پاک کردن اسپیس یا خط خالی اضافی
    /// </summary>
    public static string RemoveSpacingAndLineBreaks(this string text)
    {
        var phase1 = MatchCleanupSpacingAndLineBreaks1.Replace(text, " ");
        var phase2 = MatchCleanupSpacingAndLineBreaks2.Replace(phase1, "$1");

        return phase2.Trim();
    }
}
