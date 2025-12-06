namespace Finisher.Shared.Normalizer.Util;

public static class ToPersianNumbersUtil
{
    /// <summary>
    /// تبدیل اعداد انگلیسی به فارسی
    /// </summary>
    public static string ToPersianNumbers(this string? data)
    {
        if (data is null)
        {
            return string.Empty;
        }

        var dataChars = data.ToCharArray();
        for (var i = 0; i < dataChars.Length; i++)
        {
            switch (dataChars[i])
            {
                case '0':
                case '\u0660':
                    dataChars[i] = '\u06F0';
                    break;

                case '1':
                case '\u0661':
                    dataChars[i] = '\u06F1';
                    break;

                case '2':
                case '\u0662':
                    dataChars[i] = '\u06F2';
                    break;

                case '3':
                case '\u0663':
                    dataChars[i] = '\u06F3';
                    break;

                case '4':
                case '\u0664':
                    dataChars[i] = '\u06F4';
                    break;

                case '5':
                case '\u0665':
                    dataChars[i] = '\u06F5';
                    break;

                case '6':
                case '\u0666':
                    dataChars[i] = '\u06F6';
                    break;

                case '7':
                case '\u0667':
                    dataChars[i] = '\u06F7';
                    break;

                case '8':
                case '\u0668':
                    dataChars[i] = '\u06F8';
                    break;

                case '9':
                case '\u0669':
                    dataChars[i] = '\u06F9';
                    break;
            }
        }

        return new string(dataChars);
    }
}
