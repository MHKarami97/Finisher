using Finisher.Shared.Consts;
using Sqids;

namespace Finisher.Shared.Generator;

/// <summary>
/// Provides functionality to encode and decode numeric IDs into short, URL-safe strings
/// </summary>
public static class IdGenerator
{
    private static readonly SqidsEncoder<long> Encoder = new(new SqidsOptions { Alphabet = MainConsts.EncodedIdAlphabet, MinLength = MainConsts.EncodedIdLength });

    /// <summary>
    /// Encodes a <see cref="long"/> numeric ID into a short, URL-safe string representation.
    /// </summary>
    /// <param name="id">The numeric ID to encode.</param>
    /// <returns>A URL-safe encoded string representing the input ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="id"/> is negative.</exception>
    public static string Encode(long id)
    {
        return Encoder.Encode(id);
    }

    /// <summary>
    /// Decodes a URL-safe encoded string back into the original <see cref="long"/> numeric ID.
    /// </summary>
    /// <param name="encoded">The encoded string to decode.</param>
    /// <returns>The original numeric ID represented by the encoded string.</returns>
    /// <exception cref="FormatException">
    /// Thrown if <paramref name="encoded"/> is invalid or cannot be decoded into exactly one ID.
    /// </exception>
    public static long Decode(string encoded)
    {
        var decoded = Encoder.Decode(encoded);
        return decoded.Single();
    }

    /// <summary>
    /// Extension method that encodes an <see cref="long"/> value to a URL-safe encoded string.
    /// </summary>
    /// <param name="number">The <see cref="long"/> number to encode.</param>
    /// <returns>A URL-safe encoded string.</returns>
    public static string ToEncodedId(this long number)
    {
        return Encode(number);
    }

    /// <summary>
    /// Extension method that encodes an <see cref="int"/> value to a URL-safe encoded string.
    /// </summary>
    /// <param name="number">The <see cref="int"/> number to encode.</param>
    /// <returns>A URL-safe encoded string.</returns>
    public static string ToEncodedId(this int number)
    {
        return Encode(number);
    }

    /// <summary>
    /// Extension method that decode an URL-safe to long
    /// </summary>
    /// <param name="id">The <see cref="string"/> URL-safe to decode.</param>
    /// <returns>A long id</returns>
    public static long ToDecodedId(this string id)
    {
        return Decode(id);
    }
}
