

namespace Filter
{
    /// <summary>
    /// Interface for a filtering mechanism for collections of items of type T.
    /// <para>Keywords</para>
    /// <para>Explicitly - Registered manually using Include and Exclude methods, overriding the <see cref="Default"/> behaviour.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T> : ICloneable, IEquatable<IFilter<T>>
        where T : notnull, IEquatable<T>
    {
        #region Write Operations

        /// <summary> The default filter option. </summary>
        FilterType Default { get; set; }

        /// <summary> Overrides the current filter behavior for the specified <paramref name="item"/>, marking it to be included explicitly in the filter results. </summary>
        /// <param name="item">The item to include in the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Include(T item);

        /// <summary> Overrides the current filter behavior for <paramref name="items"/>, marking it to be included explicitly in the filter results. </summary>
        /// <param name="items">The items to include in the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Include(params T[] items);

        /// <summary> Overrides the current filter behavior for the specified <paramref name="item"/>, marking it to be excluded explicitly in the filter results. </summary>
        /// <param name="item">The item to exclude from the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Exclude(T item);

        /// <summary> Overrides the current filter behavior for <paramref name="items"/>, marking it to be excluded explicitly in the filter results. </summary>
        /// <param name="items">The items to exclude from the filter results.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> Exclude(params T[] items);

        /// <summary> Removes the specified item from the override list, causing it to revert to the default filter behavior during filtering operations. </summary>
        /// <param name="item">The item to reset to default filter behavior.</param>
        /// <returns>The filter itself.</returns>
        IFilter<T> SetAsDefault(T item);

        /// <summary> Clears all override options, reverting to the default filter behavior. </summary>
        /// <returns>The filter itself.</returns>
        IFilter<T> Clear();

        #endregion

        #region Read Operations

        #region Explicit

        /// <summary>
        /// Retrieves a list of items that are explicitly set to be included.
        /// </summary>
        /// <returns>Items set for inclusion.</returns>
        IEnumerable<T> ExplicitIncludedItems { get; }

        /// <summary>
        /// Retrieves a list of items that are explicitly set to be excluded.
        /// </summary>
        /// <returns>Items set for exclusion.</returns>
        IEnumerable<T> ExplicitExcludedItems { get; }

        /// <summary>
        /// Determines if the specified item is explicitly excluded ( <see cref="Exclude(T)"/>, <see cref="Exclude(T[])"/> ) based on the current filter settings.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is explicitly excluded; otherwise, false.</returns>
        bool IsExplicitlyExcluded(T item);

        /// <summary>
        /// Determines if the specified item is explicitly included ( <see cref="Include(T)"/>, <see cref="Include(T[])"/> ) based on the current filter settings.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item is explicitly included; otherwise, false.</returns>
        bool IsExplicitlyIncluded(T item);

        /// <summary>
        /// Determines if any of the items are explicitly included ( <see cref="Include(T)"/> or <see cref="Include(T[])"/> ) if so will return true.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool AnyExplicitIncluded(params T[] items);

        /// <summary>
        /// Determines if any of the items are explicitly excluded ( <see cref="Exclude(T)"/> or <see cref="Exclude(T[])"/> ) if so will return true.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool AnyExplicitExcluded(params T[] items);

        #endregion

        /// <summary> Determines if the specified item is included either by being explititly included ( <see cref="Include(T)"/>, <see cref="Include(T[])"/> ) or by default ( <see cref="Default"/> ) </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsIncluded(T item);

        /// <summary> Determines if the specified item is included either by being explititly excluded ( <see cref="Exclude(T)"/>, <see cref="Exclude(T[])"/> ) or by default ( <see cref="Default"/> ) </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsExcluded(T item);

        /// <summary> Any values that should be included either by <see cref="Default"/> or ( <see cref="Include(T)"/>, <see cref="Include(T[])"/> ) </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool AnyIncluded(params T[] items);

        /// <summary> Any values that should be excluded either by <see cref="Default"/> or ( <see cref="Exclude(T)"/>, <see cref="Exclude(T[])"/> ) </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool AnyExcluded(params T[] items);

        /// <summary>
        /// Will use <see cref="Default"/> and <see cref="ExplicitExcludedItems"/> if any are contained in either then will return false; else true.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool ShouldPass(params T[] items);

        /// <summary>
        /// Will use <see cref="Default"/> and <see cref="ExplicitIncludedItems"/> if any are contained in either then will return true; else false.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        bool ShouldFail(params T[] items);

        #endregion
    }
}
