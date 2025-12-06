namespace Finisher.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for <see cref="DbFunctions"/> to simplify usage
/// of database-specific functions in LINQ queries.
/// </summary>
public static class EfFunctionsExtensions
{
    /// <summary>
    /// Performs a "contains" operation using SQL LIKE.
    /// Equivalent to SQL: '%pattern%'.
    /// </summary>
    /// <param name="_">DbFunctions instance (ignored, required for extension syntax)</param>
    /// <param name="matchExpression">The string to search in.</param>
    /// <param name="pattern">The substring to find.</param>
    /// <returns>True if the expression contains the pattern.</returns>
    public static bool Contains(this DbFunctions _, string matchExpression, string pattern)
    {
        return EF.Functions.Like(matchExpression, $"%{pattern}%");
    }

    /// <summary>
    /// Performs a "starts with" operation using SQL LIKE.
    /// Equivalent to SQL: 'pattern%'.
    /// </summary>
    /// <param name="_">DbFunctions instance (ignored, required for extension syntax)</param>
    /// <param name="matchExpression">The string to search in.</param>
    /// <param name="pattern">The starting substring.</param>
    /// <returns>True if the expression starts with the pattern.</returns>
    public static bool StartsWith(this DbFunctions _, string matchExpression, string pattern)
    {
        return EF.Functions.Like(matchExpression, $"{pattern}%");
    }

    /// <summary>
    /// Performs an "ends with" operation using SQL LIKE.
    /// Equivalent to SQL: '%pattern'.
    /// </summary>
    /// <param name="_">DbFunctions instance (ignored, required for extension syntax)</param>
    /// <param name="matchExpression">The string to search in.</param>
    /// <param name="pattern">The ending substring.</param>
    /// <returns>True if the expression ends with the pattern.</returns>
    public static bool EndsWith(this DbFunctions _, string matchExpression, string pattern)
    {
        return EF.Functions.Like(matchExpression, $"%{pattern}");
    }

    /// <summary>
    /// Checks if a value exists in a set (SQL IN).
    /// Example: context.Users.Where(u => EF.Functions.In(u.Role, new[] {"Admin", "User"}))
    /// </summary>
    public static bool In<T>(this DbFunctions _, T value, params T[] set)
    {
        return set.Contains(value);
    }

    /// <summary>
    /// Returns the difference in days between two dates.
    /// SQL Server: DATEDIFF(day, start, end)
    /// </summary>
    public static int DateDiffDays(this DbFunctions _, DateTime startDate, DateTime endDate)
    {
        return EF.Functions.DateDiffDay(startDate, endDate);
    }

    /// <summary>
    /// Returns the difference in months between two dates.
    /// </summary>
    public static int DateDiffMonths(this DbFunctions _, DateTime startDate, DateTime endDate)
    {
        return EF.Functions.DateDiffMonth(startDate, endDate);
    }

    /// <summary>
    /// Returns the difference in years between two dates.
    /// </summary>
    public static int DateDiffYears(this DbFunctions _, DateTime startDate, DateTime endDate)
    {
        return EF.Functions.DateDiffYear(startDate, endDate);
    }
}
