namespace Finisher.Shared.Extensions;

public static class GuidGenerator
{
    /// <summary>
    /// Generates a new GUID string in 32-character format (no dashes).
    /// Example: d9b6b3e8724f4c4e918f01f988f72c5b
    /// </summary>
    public static string New() => Guid.NewGuid().ToString("N");

    /// <summary>
    /// Generates a raw GUID.
    /// </summary>
    public static Guid NewGuid() => Guid.NewGuid();
}

