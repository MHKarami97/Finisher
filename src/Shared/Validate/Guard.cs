using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Finisher.Shared.Validate;

public static class Guard
{
    public static void AgainstNull<T>([NotNull] T? value, string parameterName)
    {
        if (value is null)
        {
            throw new ArgumentNullException(parameterName);
        }
    }

    public static void AgainstNullOrEmpty([NotNull] string? value, string parameterName)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException(Messages.ValueCanNotBeNull, parameterName);
        }
    }

    public static void AgainstNullOrWhiteSpace([NotNull] string? value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(Messages.ValueCanNotBeNull, parameterName);
        }
    }

    public static void AgainstStringLengthIfNotNull(string? value, int minLength, int maxLength, string parameterName)
    {
        if (value is null)
        {
            return;
        }

        if (value.Length < minLength || value.Length > maxLength)
        {
            throw new ArgumentOutOfRangeException(parameterName, Messages.LengthBetween.FormatWith(minLength, maxLength, value.Length));
        }
    }

    public static void AgainstStringLengthAndNull(string? value, int minLength, int maxLength, string parameterName)
    {
        AgainstNullOrWhiteSpace(value, nameof(value));

        if (value!.Length < minLength || value.Length > maxLength)
        {
            throw new ArgumentOutOfRangeException(parameterName, Messages.LengthBetween.FormatWith(minLength, maxLength, value.Length));
        }
    }

    public static void AgainstRegex(string value, string pattern, string parameterName)
    {
        if (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500)))
        {
            throw new ArgumentException(Messages.NotMatchedPattern.FormatWith(nameof(pattern)), parameterName);
        }
    }

    public static void AgainstOutOfRange<T>(T value, T min, T max, string parameterName) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, Messages.LengthBetween.FormatWith(min, max, value));
        }
    }

    public static void AgainstInvalidEnum<TEnum>(TEnum value, string parameterName) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value))
        {
            throw new ArgumentException(Messages.InvalidEnum.FormatWith(typeof(TEnum).Name), parameterName);
        }
    }

    public static void AgainstNegativeOrZero(long value, string parameterName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, Messages.NotNegativeOrZero.FormatWith(parameterName));
        }
    }

    public static void AgainstNegativeOrZero(int value, string parameterName)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, Messages.NotNegativeOrZero.FormatWith(parameterName));
        }
    }

    public static void AgainstNegativeOrZeroInNotNull(int? value, string parameterName)
    {
        if (value is null)
        {
            return;
        }

        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, Messages.NotNegativeOrZero.FormatWith(parameterName));
        }
    }

    public static void AgainstDateRange(DateTimeOffset start, DateTimeOffset end, string startName, string endName)
    {
        if (start > end)
        {
            throw new ArgumentException(Messages.NotValidDateRange.FormatWith(startName, endName));
        }
    }

    public static void AgainstDefaultValue(DateTimeOffset value, string parameterName)
    {
        if (value == default)
        {
            throw new ArgumentException(Messages.NotDefaultValue, parameterName);
        }
    }

    public static bool IsNotEmpty<T>(this ICollection<T> collection) => collection.Count > 0;

    public static bool IsNotEmpty<T>(this IEnumerable<T> collection) => collection.Any();
}
