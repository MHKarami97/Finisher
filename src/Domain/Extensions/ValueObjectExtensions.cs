namespace Finisher.Domain.Extensions;

public static class ValueObjectExtensions
{
    public static bool IsEqualTo<T>(this T? left, T? right)
        where T : ValueObject
    {
        return left is null && right is null || (left is not null && right is not null && left.Equals(right));
    }
}
