namespace Finisher.Shared.Normalizer.Util;

public static class ArabicYeKeUtil
{
    /// <summary>
    /// Arabic Ke Char \u0643 = ARABIC LETTER KAF
    /// </summary>
    private const char ArabicKeChar = (char)1603;

    /// <summary>
    /// Arabic Ye Char \u0649 = ARABIC LETTER ALEF MAKSURA
    /// </summary>
    private const char ArabicYeChar1 = (char)1609;

    /// <summary>
    /// Arabic Ye Char \u064A = ARABIC LETTER YEH
    /// </summary>
    private const char ArabicYeChar2 = (char)1610;

    /// <summary>
    /// ؠ
    /// </summary>
    private const char ArabicYeWithOneDotBelow = (char)1568;

    /// <summary>
    /// ؽ
    /// </summary>
    private const char ArabicYeWithInvertedV = (char)1597;

    /// <summary>
    /// ؾ
    /// </summary>
    private const char ArabicYeWithTwoDotsAbove = (char)1598;

    /// <summary>
    /// ؿ
    /// </summary>
    private const char ArabicYeWithThreeDotsAbove = (char)1599;

    /// <summary>
    /// ٸ
    /// </summary>
    private const char ArabicYeWithHighHamzeYeh = (char)1656;

    /// <summary>
    /// ې
    /// </summary>
    private const char ArabicYeWithFinalForm = (char)1744;

    /// <summary>
    /// ۑ
    /// </summary>
    private const char ArabicYeWithThreeDotsBelow = (char)1745;

    /// <summary>
    /// ۍ
    /// </summary>
    private const char ArabicYeWithTail = (char)1741;

    /// <summary>
    /// ێ
    /// </summary>
    private const char ArabicYeSmallV = (char)1742;

    /// <summary>
    /// Persian Ke Char \u06A9 = ARABIC LETTER KEHEH
    /// </summary>
    private const char PersianKeChar = (char)1705;

    /// <summary>
    /// Persian Ye Char \u06CC = 'ARABIC LETTER FARSI YEH
    /// </summary>
    private const char PersianYeChar = (char)1740;

    /// <summary>
    /// تبدیل کاراکتر عربی ی ک به فارسی
    /// </summary>
    public static string ConvertArabicYeKe(this string? data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return string.Empty;
        }

        var dataChars = data.ToCharArray();
        for (var i = 0; i < dataChars.Length; i++)
        {
            switch (dataChars[i])
            {
                case ArabicYeChar1:
                case ArabicYeChar2:
                case ArabicYeWithOneDotBelow:
                case ArabicYeWithInvertedV:
                case ArabicYeWithTwoDotsAbove:
                case ArabicYeWithThreeDotsAbove:
                case ArabicYeWithHighHamzeYeh:
                case ArabicYeWithFinalForm:
                case ArabicYeWithThreeDotsBelow:
                case ArabicYeWithTail:
                case ArabicYeSmallV:
                    dataChars[i] = PersianYeChar;
                    break;

                case ArabicKeChar:
                    dataChars[i] = PersianKeChar;
                    break;
            }
        }

        return new string(dataChars);
    }
}
