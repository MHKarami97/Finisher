using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class UtsideInsideSpacingUtil
{
    private static readonly Regex MatchRemoveOutsideInsideSpacing1 = new(@"[   ‌]*(\()\s*([^)]+?)\s*?(\))[   ‌]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing10 = new(@"(\{)\s*([^)]+?)\s*?(\})",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing11 = new(@"(“)\s*([^)]+?)\s*?(”)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing12 = new(@"(«)\s*([^)]+?)\s*?(»)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing2 = new(@"[   ‌]*(\[)\s*([^)]+?)\s*?(\])[   ‌]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing3 = new(@"[   ‌]*(\{)\s*([^)]+?)\s*?(\})[   ‌]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing4 = new(@"[   ‌]*(“)\s*([^)]+?)\s*?(”)[   ‌]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing5 = new(@"[   ‌]*(«)\s*([^)]+?)\s*?(»)[   ‌]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing6 = new(@"[ ?  ]*([:;,??.?!]{1})[ ?  ]*",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing7 = new(@"([0-9]+):\s+([0-9]+)",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing8 = new(@"(\()\s*([^)]+?)\s*?(\))",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    private static readonly Regex MatchRemoveOutsideInsideSpacing9 = new(@"(\[)\s*([^)]+?)\s*?(\])",
        RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// درست کردن فاصله اضافی در داخل و بیرون () [] {}  “” «»
    /// </summary>
    public static string OutsideInsideSpacing(this string text)
    {
        //should fix outside and inside spacing for () [] {}  “” «»
        var phase1 = MatchRemoveOutsideInsideSpacing1.Replace(text, " $1$2$3 ");
        var phase2 = MatchRemoveOutsideInsideSpacing2.Replace(phase1, " $1$2$3 ");
        var phase3 = MatchRemoveOutsideInsideSpacing3.Replace(phase2, " $1$2$3 ");
        var phase4 = MatchRemoveOutsideInsideSpacing4.Replace(phase3, " $1$2$3 ");
        var phase5 = MatchRemoveOutsideInsideSpacing5.Replace(phase4, " $1$2$3 ");

        // : ; , . ! ? and their Persian equivalents should have one space after and no space before
        var phase6 = MatchRemoveOutsideInsideSpacing6.Replace(phase5, "$1 ");

        // do not put space after colon that separates time parts
        var phase7 = MatchRemoveOutsideInsideSpacing7.Replace(phase6, "$1:$2");

        //should fix inside spacing for () [] {}  “” «»
        var phase8 = MatchRemoveOutsideInsideSpacing8.Replace(phase7, "$1$2$3");
        var phase9 = MatchRemoveOutsideInsideSpacing9.Replace(phase8, "$1$2$3");
        var phase10 = MatchRemoveOutsideInsideSpacing10.Replace(phase9, "$1$2$3");
        var phase11 = MatchRemoveOutsideInsideSpacing11.Replace(phase10, "$1$2$3");
        var phase12 = MatchRemoveOutsideInsideSpacing12.Replace(phase11, "$1$2$3");

        return phase12.Trim();
    }
}
