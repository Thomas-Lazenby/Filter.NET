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

        private readonly Dictionary<T, FilterType> _filterItems = new();

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


        public bool ShouldInclude(T item)
        {
            if (_filterItems.TryGetValue(item, out var filterType))
                return filterType == FilterType.Include;

            return Default == FilterType.Include;
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

        public IFilter<T> SetAsDefault(T item)
        {
            _filterItems.Remove(item);
            return this;
        }
    }
}
