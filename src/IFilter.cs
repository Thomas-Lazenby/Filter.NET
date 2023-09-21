

namespace Filter
{
    /// <summary>
    /// Interface for a filtering mechanism for collections of items of type T. Filter's are <see cref="Filter{T}"/> and ConcurrentFilter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T> : ICloneable, IEquatable<IFilter<T>>
        where T : notnull, IEquatable<T>
    {
        /// <summary> The default filter option. </summary>
        FilterType Default { get; set; }

        /// <summary> Overrides the current default behavior for the specified item, marking it to be included in the filter results. </summary>
        /// <param name="item">The item to include in the filter results.</param>
        IFilter<T> Include(T item);

        /// <summary>
        /// Overrides the current default behavior for the specified item, marking it to be excluded from the filter results.
        /// </summary>
        /// <param name="item">The item to exclude from the filter results.</param>
        IFilter<T> Exclude(T item);

        /// <summary>
        /// Determines whether the specified item should be included in the filter results based on the current filter settings.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>true if the item should be included; otherwise, false.</returns>
        bool ShouldInclude(T item);

        /// <summary>
        /// Removes the specified item from the override list, causing it to revert to the default filter behavior during filtering operations.
        /// </summary>
        /// <param name="item">The item to reset to default filter behavior.</param>
        /// <returns>true if the item was found and removed from the override list; otherwise, false.</returns>
        IFilter<T> SetAsDefault(T item);

        /// <summary>
        /// Clears all override options, reverting to the default filter behavior.
        /// </summary>
        void Clear();
    }
}
