using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Finisher.Shared.Extensions;

/// <summary>
/// Extension methods for String class.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Compares two strings for equality with configurable comparison options.
    /// </summary>
    /// <param name="source">The first string.</param>
    /// <param name="target">The second string.</param>
    /// <param name="comparison">The string comparison method. Defaults to Ordinal.</param>
    public static bool EqualsTo(this string? source, string? target, StringComparison comparison = StringComparison.Ordinal)
    {
        return string.Equals(source, target, comparison);
    }

    /// <summary>
    /// Ensure start with given string if it does not ends with the char.
    /// </summary>
    public static bool EnsureEndsWith(this string str, char c)
    {
        return EnsureEndsWith(str, c, StringComparison.Ordinal);
    }

    /// <summary>
    /// Ensure start with given string if it does not ends with the char.
    /// </summary>
    public static bool EnsureEndsWith(this string str, char c, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.EndsWith(c.ToString(), comparisonType))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ensure start with given string if it does not ends with the char.
    /// </summary>
    public static bool EnsureEndsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.EndsWith(c.ToString(culture), ignoreCase, culture))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ensure start with given string if it does not starts with the char.
    /// </summary>
    public static bool EnsureStartsWith(this string str, char c)
    {
        return EnsureStartsWith(str, c, StringComparison.Ordinal);
    }

    /// <summary>
    /// Ensure start with given string if it does not starts with the char.
    /// </summary>
    public static bool EnsureStartsWith(this string str, string c)
    {
        return EnsureStartsWith(str, c, StringComparison.Ordinal);
    }

    /// <summary>
    /// Ensure start with given string if it does not starts with the char.
    /// </summary>
    public static bool EnsureStartsWith(this string str, char c, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.StartsWith(c.ToString(), comparisonType))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///Ensure start with given string if it does not starts with the char.
    /// </summary>
    public static bool EnsureStartsWith(this string str, string c, StringComparison comparisonType)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.StartsWith(c, comparisonType))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Ensure start with given string if it does not starts with the char.
    /// </summary>
    public static string EnsureStartsWith(this string str, char c, bool ignoreCase, CultureInfo culture)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.StartsWith(c.ToString(culture), ignoreCase, culture))
        {
            return str;
        }

        return c + str;
    }

    /// <summary>
    /// Indicates whether this string is null or an System.String.Empty string.
    /// </summary>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// indicates whether this string is null, empty, or consists only of white-space characters.
    /// </summary>
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
    public static string? Left(this string? str, int len)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str.Substring(0, len);
    }

    /// <summary>
    /// Converts line endings in the string to <see cref="Environment.NewLine"/>.
    /// </summary>
    public static string NormalizeLineEndings(this string str)
    {
        return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// Gets index of nth occurence of a char in a string.
    /// </summary>
    /// <param name="str">source string to be searched</param>
    /// <param name="c">Char to search in <paramref name="str"/></param>
    /// <param name="n">Count of the occurence</param>
    public static int NthIndexOf(this string str, char c, int n)
    {
        ArgumentNullException.ThrowIfNull(str);

        var count = 0;
        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] != c)
            {
                continue;
            }

            if ((++count) == n)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// Removes first occurrence of the given postfixes from end of the given string.
    /// Ordering is important. If one of the postFixes is matched, others will not be tested.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="postFixes">one or more postfix.</param>
    /// <returns>Modified string or the same string if it has not any of given postfixes</returns>
    public static string? RemovePostFix(this string? str, params string[] postFixes)
    {
        if (str == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        if (postFixes.IsNullOrEmpty())
        {
            return str;
        }

        foreach (var postFix in postFixes)
        {
            if (str.EndsWith(postFix, StringComparison.OrdinalIgnoreCase))
            {
                return str.Left(str.Length - postFix.Length);
            }
        }

        return str;
    }

    /// <summary>
    /// Removes first occurrence of the given prefixes from beginning of the given string.
    /// Ordering is important. If one of the preFixes is matched, others will not be tested.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="preFixes">one or more prefix.</param>
    /// <returns>Modified string or the same string if it has not any of given prefixes</returns>
    public static string? RemovePreFix(this string? str, params string[] preFixes)
    {
        if (str == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(str))
        {
            return string.Empty;
        }

        if (preFixes.IsNullOrEmpty())
        {
            return str;
        }

        foreach (var preFix in preFixes)
        {
            if (str.StartsWith(preFix, StringComparison.OrdinalIgnoreCase))
            {
                return str.Right(str.Length - preFix.Length);
            }
        }

        return str;
    }

    /// <summary>
    /// Gets a substring of a string from end of the string.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="len"/> is bigger that string's length</exception>
    public static string? Right(this string? str, int len)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (str.Length < len)
        {
            throw new ArgumentException("len argument can not be bigger than given string's length!");
        }

        return str.Substring(str.Length - len, len);
    }

    /// <summary>
    /// Uses string.Split method to split given string by given separator.
    /// </summary>
    public static string[] Split(this string str, string separator)
    {
        return str.Split(new[] { separator }, StringSplitOptions.None);
    }

    /// <summary>
    /// Uses string.Split method to split given string by given separator.
    /// </summary>
    public static string[] Split(this string str, string separator, StringSplitOptions options)
    {
        return str.Split(new[] { separator }, options);
    }

    /// <summary>
    /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
    /// </summary>
    public static string[] SplitToLines(this string str)
    {
        return str.Split(Environment.NewLine);
    }

    /// <summary>
    /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
    /// </summary>
    public static string[] SplitToLines(this string str, StringSplitOptions options)
    {
        return str.Split(Environment.NewLine, options);
    }

    /// <summary>
    /// Converts PascalCase string to camelCase string.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="invariantCulture">Invariant culture</param>
    /// <returns>camelCase of the string</returns>
    public static string ToCamelCase(this string str, bool invariantCulture = true)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return invariantCulture ? str.ToUpperInvariant() : str.ToUpper(CultureInfo.InvariantCulture);
        }

        return (invariantCulture ? char.ToLowerInvariant(str[0]) : char.ToLower(str[0], CultureInfo.InvariantCulture)) + str.Substring(1);
    }

    /// <summary>
    /// Converts PascalCase string to camelCase string in specified culture.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="culture">An object that supplies culture-specific casing rules</param>
    /// <returns>camelCase of the string</returns>
    public static string ToCamelCase(this string str, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return str.ToLower(culture);
        }

        return char.ToLower(str[0], culture) + str.Substring(1);
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to sentence (by splitting words by space).
    /// Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="invariantCulture">Invariant culture</param>
    public static string ToSentenceCase(this string str, bool invariantCulture = false)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return Regex.Replace(
            str,
            "[a-z][A-Z]",
            m => m.Value[0] + " " + (invariantCulture ? char.ToLowerInvariant(m.Value[1]) : char.ToLower(m.Value[1], CultureInfo.InvariantCulture))
        );
    }

    /// <summary>
    /// Converts given PascalCase/camelCase string to sentence (by splitting words by space).
    /// Example: "ThisIsSampleSentence" is converted to "This is a sample sentence".
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <param name="culture">An object that supplies culture-specific casing rules.</param>
    public static string ToSentenceCase(this string str, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1], culture));
    }

    /// <summary>
    /// Converts string to enum value.
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(value);

        return Enum.Parse<T>(value);
    }

    /// <summary>
    /// Converts string to enum value.
    /// </summary>
    /// <typeparam name="T">Type of enum</typeparam>
    /// <param name="value">String value to convert</param>
    /// <param name="ignoreCase">Ignore case</param>
    /// <returns>Returns enum object</returns>
    public static T ToEnum<T>(this string value, bool ignoreCase)
        where T : struct
    {
        ArgumentNullException.ThrowIfNull(value);

        return Enum.Parse<T>(value, ignoreCase);
    }

    public static string ToMd5(this string str)
    {
        var inputBytes = Encoding.UTF8.GetBytes(str);
        var hashBytes = MD5.HashData(inputBytes);

        var sb = new StringBuilder();
        foreach (var hashByte in hashBytes)
        {
            sb.Append(hashByte.ToString("X2", CultureInfo.InvariantCulture));
        }

        return sb.ToString();
    }

    /// <summary>
    /// Converts camelCase string to PascalCase string.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="invariantCulture">Invariant culture</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, bool invariantCulture = true)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return invariantCulture ? str.ToUpperInvariant() : str.ToUpper(CultureInfo.InvariantCulture);
        }

        return (invariantCulture ? char.ToUpperInvariant(str[0]) : char.ToUpper(str[0], CultureInfo.InvariantCulture)) + str.Substring(1);
    }

    /// <summary>
    /// Converts camelCase string to PascalCase string in specified culture.
    /// </summary>
    /// <param name="str">String to convert</param>
    /// <param name="culture">An object that supplies culture-specific casing rules</param>
    /// <returns>PascalCase of the string</returns>
    public static string ToPascalCase(this string str, CultureInfo culture)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return str;
        }

        if (str.Length == 1)
        {
            return str.ToUpper(culture);
        }

        return char.ToUpper(str[0], culture) + str.Substring(1);
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string? Truncate(this string? str, int maxLength)
    {
        if (str == null)
        {
            return null;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        return str.Left(maxLength);
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// It adds a "..." postfix to end of the string if it's truncated.
    /// Returning string can not be longer than maxLength.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string? TruncateWithPostfix(this string? str, int maxLength)
    {
        return TruncateWithPostfix(str, maxLength, "...");
    }

    /// <summary>
    /// Gets a substring of a string from beginning of the string if it exceeds maximum length.
    /// It adds given <paramref name="postfix"/> to end of the string if it's truncated.
    /// Returning string can not be longer than maxLength.
    /// </summary>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="str"/> is null</exception>
    public static string? TruncateWithPostfix(this string? str, int maxLength, string postfix)
    {
        if (str == null)
        {
            return null;
        }

        if (string.IsNullOrEmpty(str) || maxLength == 0)
        {
            return string.Empty;
        }

        if (str.Length <= maxLength)
        {
            return str;
        }

        if (maxLength <= postfix.Length)
        {
            return postfix.Left(maxLength);
        }

        return str.Left(maxLength - postfix.Length) + postfix;
    }

    /// <summary>
    /// Converts given string to a byte array using <see cref="Encoding.UTF8"/> encoding.
    /// </summary>
    public static byte[] GetBytes(this string str)
    {
        return str.GetBytes(Encoding.UTF8);
    }

    /// <summary>
    /// Converts given string to a byte array using the given <paramref name="encoding"/>
    /// </summary>
    public static byte[] GetBytes([NotNull] this string str, [NotNull] Encoding encoding)
    {
        Check.NotNull(str, nameof(str));
        Check.NotNull(encoding, nameof(encoding));

        return encoding.GetBytes(str);
    }

    /// <summary>
    /// Format string
    /// </summary>
    /// <param name="format"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static string FormatWith(this string format, params object[] args)
    {
        return string.IsNullOrWhiteSpace(format)
            ? throw new ArgumentNullException(nameof(format))
            : string.Format(CultureInfo.InvariantCulture, format, args);
    }

    /// <summary>
    /// Converts an object to a lowercase string. Returns empty string if null.
    /// </summary>
    public static string ToLowerString(this object? value)
        => value?.ToString()?.ToUpperInvariant() ?? string.Empty;

    /// <summary>
    /// Converts a string to a slug: trims, lowercases, and replaces spaces with hyphens.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <returns>A cleaned slug suitable for URLs.</returns>
    public static string ToSlug(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var result = input.Trim().ToUpperInvariant();

        // Replace multiple spaces (or any whitespace) with a single hyphen
        result = Regex.Replace(result, @"\s+", "-");

        return result;
    }
}
