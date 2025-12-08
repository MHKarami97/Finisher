namespace Finisher.Shared.Normalizer;

/// <summary>
/// کانفیگ
/// </summary>
public class NormalizerConfig
{
    /// <summary>
    /// پاک کردن فاصله اضافی
    /// </summary>
    public bool Trim { get; init; } = true;

    /// <summary>
    /// حذف اعراب از حروف
    /// </summary>
    public bool RemoveDiacritics { get; init; } = true;

    /// <summary>
    /// تبدیل حروف ی و ک عربی به فارسی
    /// </summary>
    public bool ConvertArabicYeKe { get; init; } = true;

    /// <summary>
    /// درست کردن نیم فاصله در کلمه
    /// </summary>
    public bool ChangeToHalfSpace { get; init; } = true;

    /// <summary>
    /// پاک کردن دش های اضافه بین کلمات
    /// </summary>
    public bool RemoveMoreDash { get; init; } = true;

    /// <summary>
    /// ک کردن نقطه اضافه
    /// </summary>
    public bool RemoveMoreDot { get; init; } = true;

    /// <summary>
    /// تبدیل نقله قول های انگلیسی به فارسی
    /// </summary>
    public bool ConvertEnglishQuotes { get; init; } = true;

    /// <summary>
    /// پاک کردن ؟ ! اضافی
    /// </summary>
    public bool RemoveExtraMarks { get; init; } = true;

    /// <summary>
    /// تبدیل حروف کشیده به عادی
    /// </summary>
    public bool RemoveKeshide { get; init; } = true;

    /// <summary>
    /// پاک کردن اسپیس و خط خالی اضافی
    /// </summary>
    public bool RemoveSpacingAndLineBreaks { get; init; } = true;

    /// <summary>
    /// درست کردن فاصله اضافی در داخل و بیرون () [] {}  “” «»
    /// </summary>
    public bool OutsideInsideSpacing { get; init; } = true;

    /// <summary>
    /// پاک کردن حروف غیر قابل نمایش
    /// </summary>
    public bool RemoveHexadecimalSymbols { get; init; } = true;

    /// <summary>
    /// تبدیل اعداد
    /// </summary>
    public NumberConvertorType NumberConvertorType { get; init; } = NumberConvertorType.ToEnglish;
}
