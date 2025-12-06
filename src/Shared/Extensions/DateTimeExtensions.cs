using System.Globalization;

namespace Finisher.Shared.Extensions;

public static class DateTimeExtensions
{
    public static string ToFormattedString(this DateTimeOffset dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
