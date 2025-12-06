using System.Collections;

namespace Finisher.Shared.Extensions;

/// <summary>
/// Extension methods for Collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Checks whatever given collection object is null or has no item.
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T>? source)
    {
        return source is not { Count: > 0 };
    }

    /// <summary>
    /// Determines whether the specified <see cref="IEnumerable"/> is null or contains no elements.
    /// </summary>
    /// <param name="source">The enumerable to check.</param>
    /// <returns><c>true</c> if the enumerable is null or empty; otherwise, <c>false</c>.</returns>
    public static bool IsNullOrEmpty(this IEnumerable? source)
    {
        return source == null || !source.GetEnumerator().MoveNext();
    }

    /// <summary>
    /// Adds an item to the collection if it does not already exist.
    /// Similar to <see cref="AddIfNotExists{T}"/>, but does not return a value.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to add the item to.</param>
    /// <param name="item">The item to add.</param>
    public static void AddIfNotExists<T>(this ICollection<T> collection, T item)
    {
        ArgumentNullException.ThrowIfNull(collection);

        if (!collection.Contains(item))
        {
            collection.Add(item);
        }
    }

    /// <summary>
    /// Removes an item from the collection if it exists.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to remove the item from.</param>
    /// <param name="item">The item to remove.</param>
    public static void RemoveIfExists<T>(this ICollection<T> collection, T item)
    {
        collection.Remove(item);
    }

    /// <summary>
    /// Adds a range of items to the collection, ignoring items that already exist in the collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="collection">The collection to add items to.</param>
    /// <param name="items">The items to add.</param>
    public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            collection.AddIfNotExists(item);
        }
    }
}
