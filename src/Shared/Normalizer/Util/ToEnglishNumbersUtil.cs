namespace Finisher.Shared.Normalizer.Util;

public static class ToEnglishNumbersUtil
{
    /// <summary>
    /// تبدیل اعداد فارسی و عربی به انگلیسی
    /// </summary>
    public static string ToEnglishNumbers(this string? data)
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
                case '\u06F0':
                case '\u0660':
                    dataChars[i] = '0';
                    break;

                case '\u06F1':
                case '\u0661':
                    dataChars[i] = '1';
                    break;

                case '\u06F2':
                case '\u0662':
                    dataChars[i] = '2';
                    break;

                case '\u06F3':
                case '\u0663':
                    dataChars[i] = '3';
                    break;

                case '\u06F4':
                case '\u0664':
                    dataChars[i] = '4';
                    break;

                case '\u06F5':
                case '\u0665':
                    dataChars[i] = '5';
                    break;

                case '\u06F6':
                case '\u0666':
                    dataChars[i] = '6';
                    break;

                case '\u06F7':
                case '\u0667':
                    dataChars[i] = '7';
                    break;

                case '\u06F8':
                case '\u0668':
                    dataChars[i] = '8';
                    break;

                case '\u06F9':
                case '\u0669':
                    dataChars[i] = '9';
                    break;
            }
        }

        return new string(dataChars);
    }
}
