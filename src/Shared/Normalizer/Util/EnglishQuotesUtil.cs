using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class EnglishQuotesUtil
{
    private static readonly Regex MatchConvertEnglishQuotes =
        new(@"([""'`]+)(.+?)(\1)", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// تبدیل نقله قول های انگلیسی به فارسی
    /// It converts 'تست' to «تست»
    /// </summary>
    public static string EnglishQuotesToPersian(this string text) => MatchConvertEnglishQuotes.Replace(text, "«$2»");
}
