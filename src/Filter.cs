using System;
using System.Collections.Generic;

namespace Filter
{
    /// <summary>
    /// Provides a filtering mechanism for collections of items of type T.
    /// </summary>
    /// <typeparam name="T">The type of items to be filtered.</typeparam>
    public class Filter<T> : IFilter<T>
        where T : notnull, IEquatable<T>
    {
        public FilterType Default { get; set; }

        // No beneifical case of using two hashsets instead?
        private readonly Dictionary<T, FilterType> _filterItems = new();

        public bool Equals(IFilter<T>? other)
        {
            if (other == null)
                return false;

            if (Default != other.Default)
                return false;


            if (ExplicitExcludedItems.Count() != other.ExplicitExcludedItems.Count() || ExplicitExcludedItems.Union(other.ExplicitIncludedItems).Distinct().Any()) return false;

            if (ExplicitIncludedItems.Count() != other.ExplicitIncludedItems.Count() || ExplicitIncludedItems.Union(other.ExplicitIncludedItems).Distinct().Any()) return false;


            return true;
        }

        #region Write Operations

        public IFilter<T> Exclude(T item)
        {
            _filterItems[item] = FilterType.Exclude;
            return this;
        }

        public IFilter<T> Exclude(params T[] items)
        {
            foreach (var item in items)
                _filterItems[item] = FilterType.Exclude;
            return this;
        }

        public IFilter<T> Include(T item)
        {
            _filterItems[item] = FilterType.Include;
            return this;
        }

        public IFilter<T> Include(params T[] items)
        {
            foreach (var item in items)
                _filterItems[item] = FilterType.Include;
            return this;
        }

        public IFilter<T> SetAsDefault(T item)
        {
            _filterItems.Remove(item);
            return this;
        }

        public IFilter<T> Clear()
        {
            _filterItems.Clear();
            return this;
        }

        public object Clone()
        {
            var filter = new Filter<T>();

            filter.Default = Default;

            foreach (var pair in _filterItems)
                filter._filterItems.Add(pair.Key, pair.Value);

            return filter;
        }

        #endregion

        #region Read Operations

        #region Explicit

        public IEnumerable<T> ExplicitIncludedItems => _filterItems.Where( x => x.Value == FilterType.Include).Select( x => x.Key );

        public IEnumerable<T> ExplicitExcludedItems => _filterItems.Where(x => x.Value == FilterType.Exclude).Select(x => x.Key);

        public bool IsExplicitlyIncluded(T item) => _filterItems.TryGetValue(item, out var included) && included == FilterType.Include;

        public bool IsExplicitlyExcluded(T item) => _filterItems.TryGetValue(item, out var excluded) && excluded == FilterType.Exclude;


        public bool AnyExplicitIncluded(params T[] items)
        {
            // Worse case: O(N)
            foreach(var item in items)
                if( IsExplicitlyIncluded(item) )
                    return true;

            return false;
        }

        public bool AnyExplicitExcluded(params T[] items)
        {
            // Worse case: O(N)
            foreach (var item in items)
                if ( IsExplicitlyExcluded(item) )
                    return true;

            return false;
        }

        #endregion

        public bool IsIncluded(T item) => _filterItems.TryGetValue(item, out var included) ? included == FilterType.Include : Default == FilterType.Include;

        public bool IsExcluded(T item) => _filterItems.TryGetValue(item, out var excluded) ? excluded == FilterType.Exclude : Default == FilterType.Exclude;

        public bool AnyIncluded(params T[] items)
        {
            // Worse case: O(N)
            foreach (var item in items)
                if (IsIncluded(item))
                        return true;

            return false;
        }

        public bool AnyExcluded(params T[] items)
        {
            // Worse case: O(N)
            foreach (var item in items)
                if (IsExcluded(item))
                    return true;

            return false;
        }

        #endregion
    }
}
