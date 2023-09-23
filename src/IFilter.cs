

namespace Filter
{
    /// <summary>
    /// Interface for a filtering mechanism for collections of items of type T. Filter's are <see cref="Filter{T}"/> and ConcurrentFilter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T> : ICloneable, IEquatable<IFilter<T>>
        where T : notnull, IEquatable<T>
    {
        #region Write Operations
        /// <summary> The default filter option. </summary>
        FilterType Default { get; set; }

        /// <summary> Overrides the current default behavior for the specified item, marking it to be included in the filter results. </summary>
        /// <param name="item">The item to include in the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Include(T item);

        /// <summary> Overrides the current default behavior for the specified items, marking them to be included in the filter results. </summary>
        /// <param name="items">The items to include in the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Include(params T[] items);

        /// <summary>
        /// Overrides the current default behavior for the specified item, marking it to be excluded from the filter results.
        /// </summary>
        /// <param name="item">The item to exclude from the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Exclude(T item);

        /// <summary>
        /// Overrides the current default behaviours for the specified items, marking them to be excluded from the filter results.
        /// </summary>
        /// <param name="items">The items to exclude from the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Exclude(params T[] items);

        /// <summary>
        /// Removes the specified item from the override list, causing it to revert to the default filter behavior during filtering operations.
        /// </summary>
        /// <param name="item">The item to reset to default filter behavior.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> SetAsDefault(T item);

        /// <summary> Clears all override options, reverting to the default filter behavior. </summary>
        /// <returns>The filter itself.</returns>
        IFilter<T> Clear();

        #endregion

        #region Read Operations

        /// <summary>
        /// Determines if the specified item is explicitly excluded based on the current filter settings.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is explicitly excluded; otherwise, false.</returns>
        bool IsExplicitlyExcluded(T item);

        /// <summary>
        /// Determines if the specified item is explicitly included based on the current filter settings.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is explicitly included; otherwise, false.</returns>
        bool IsExplicitlyIncluded(T item);



        /// <summary>
        /// Retrieves a list of items that are explicitly set to be included.
        /// </summary>
        /// <returns>Items set for inclusion.</returns>
        IEnumerable<T> IncludedItems { get; }

        /// <summary>
        /// Retrieves a list of items that are explicitly set to be excluded.
        /// </summary>
        /// <returns>Items set for exclusion.</returns>
        IEnumerable<T> ExcludedItems { get; }

        /// <summary>
        /// Determines which of the specified items should be included based on the current filter settings.
        /// </summary>
        /// <param name="items">The items to check.</param>
        /// <returns>A dictionary where the key is the item and the value is a boolean indicating if it should be included.</returns>
        IReadOnlyDictionary<T, bool> AreIncluded(params T[] items);

        /// <summary>
        /// Determines whether the specified item should be included in the filter results based on the current filter settings.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item should be included; otherwise, false.</returns>
        bool IsIncluded(T item);

        #endregion
    }
}
