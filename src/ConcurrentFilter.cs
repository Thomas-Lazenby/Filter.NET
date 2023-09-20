using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Filter
{
    /// <summary>
    /// Provides a filtering mechanism for collections of items of type T.
    /// </summary>
    /// <typeparam name="T">The type of items to be filtered.</typeparam>
    public class ConcurrentFilter<T> : IFilter<T>
        where T : notnull, IEquatable<T>
    {
        public FilterType Default { get; set; }

        private readonly ConcurrentDictionary<T, FilterType> _filterItems = new();

        public bool Equals(IFilter<T>? other)
        {
            if (other == null)
                return false;

            if (Default != other.Default)
                return false;

            foreach (var pair in _filterItems)
            {
                if (pair.Value == FilterType.Include && !other.ShouldInclude(pair.Key))
                    return false;

                if (pair.Value == FilterType.Exclude && other.ShouldInclude(pair.Key))
                    return false;
            }

            return true;
        }

        public void Exclude(T item)
            => _filterItems[item] = FilterType.Exclude;

        public void Include(T item)
            => _filterItems[item] = FilterType.Include;

        public bool ShouldInclude(T item)
        {
            if (_filterItems.TryGetValue(item, out var filterType))
                return filterType == FilterType.Include;

            return Default == FilterType.Include;
        }

        public void Clear()
            => _filterItems.Clear();

        public object Clone()
        {
            var filter = new ConcurrentFilter<T>();

            filter.Default = Default;

            foreach (var pair in _filterItems)
                filter._filterItems.TryAdd(pair.Key, pair.Value);

            return filter;
        }
    }
}
