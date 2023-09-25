

namespace Filter
{
    /// <summary>
    /// Interface for a filtering mechanism for collections of items of type T.
    /// <para>Keywords</para>
    /// <para>Explicitly - Registered manually using Include and Exclude methods, overriding the <see cref="Default"/> behaviour.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T> : ICloneable, IEquatable<IFilter<T>>, IReadOnlyFilter<T>
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

    }
}
