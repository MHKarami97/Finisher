using Finisher.Shared.Normalizer.Util;

namespace Finisher.Shared.Normalizer;

public static class NormalizeText
{
    /// <summary>
    /// نرمال کردن متن فارسی برای ذخیره و جستجو بهینه
    /// </summary>
    public static string NormalizerText(this string text, NormalizerConfig? config = null)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        config ??= new NormalizerConfig();

        if (config.Trim)
        {
            text = text.Trim();
        }

        text = config.NumberConvertorType switch
        {
            NumberConvertorType.ToPersian => text.ToPersianNumbers(),
            NumberConvertorType.ToEnglish => text.ToEnglishNumbers(),
            NumberConvertorType.None => text,
            _ => text
        };

        if (config.RemoveDiacritics)
        {
            text = text.RemoveDiacritics();
        }

        if (config.ConvertArabicYeKe)
        {
            text = text.ConvertArabicYeKe();
        }

        if (config.ChangeToHalfSpace)
        {
            text = text.ApplyHalfSpaceRule();
        }

        if (config.RemoveMoreDash)
        {
            text = text.NormalizeDashes();
        }

        if (config.RemoveMoreDot)
        {
            text = text.NormalizeDots();
        }

        if (config.ConvertEnglishQuotes)
        {
            text = text.EnglishQuotesToPersian();
        }

        if (config.RemoveExtraMarks)
        {
            text = text.RemoveExtraMarks();
        }

        if (config.RemoveKeshide)
        {
            text = text.RemoveAllKKeshide();
        }

        if (config.RemoveSpacingAndLineBreaks)
        {
            text = text.RemoveSpacingAndLineBreaks();
        }

        if (config.OutsideInsideSpacing)
        {
            text = text.OutsideInsideSpacing();
        }

        if (config.RemoveHexadecimalSymbols)
        {
            text = text.RemoveHexadecimalSymbols();
        }

        return text;
    }
}
