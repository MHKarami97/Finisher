using System.Text.RegularExpressions;

namespace Finisher.Shared.Normalizer.Util;

public static class Keshide
{
    private static readonly Regex MatchRemoveAllKashida =
        new("ـ+", RegexOptions.Compiled | RegexOptions.IgnoreCase, RegexUtil.MatchTimeout);

    /// <summary>
    /// درست کردن حروف کشیده
    /// تبدیل تــــــــست به تست
    /// </summary>
    public static string RemoveAllKKeshide(this string text)
        => MatchRemoveAllKashida.Replace(text, "").NormalizeUnderLines();

    private static string NormalizeUnderLines(this string? text)
    {
        if (text is null)
        {
            return string.Empty;
        }

        const char chr1600 = (char)1600; //ـ=1600
#if NET8_0_OR_GREATER
        return text.Replace(chr1600.ToString(), "", StringComparison.OrdinalIgnoreCase);
#else
		return text.Replace(Chr1600.ToString(), "");
#endif
    }
}
