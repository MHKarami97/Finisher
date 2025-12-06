namespace Finisher.Shared.Extensions;

using System.Globalization;

public static class FormatExtensions
{
    public static string ToInvariantString(this int value) =>
        value.ToString(CultureInfo.InvariantCulture);

    public static string ToInvariantString(this long value) =>
        value.ToString(CultureInfo.InvariantCulture);

    public static string ToInvariantString(this double value) =>
        value.ToString(CultureInfo.InvariantCulture);

    public static string ToInvariantString(this decimal value) =>
        value.ToString(CultureInfo.InvariantCulture);

    public static string ToInvariantString(this float value) =>
        value.ToString(CultureInfo.InvariantCulture);
}
