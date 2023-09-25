﻿using System;
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

            foreach (var pair in _filterItems)
            {
                if (pair.Value == FilterType.Include && !other.IsIncluded(pair.Key))
                    return false;

                if (pair.Value == FilterType.Exclude && other.IsIncluded(pair.Key))
                    return false;
            }

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

        public IEnumerable<T> ExplicitIncludedItems => throw new NotImplementedException();

        public IEnumerable<T> ExplicitExcludedItems => throw new NotImplementedException();

        public bool IsExplicitlyExcluded(T item)
        {
            throw new NotImplementedException();
        }

        public bool IsExplicitlyIncluded(T item)
        {
            throw new NotImplementedException();
        }

        public bool IsIncluded(T item)
        {
            throw new NotImplementedException();
        }

        public bool IsExcluded(T item)
        {
            throw new NotImplementedException();
        }

        public bool ShouldPass(params T[] items)
        {
            throw new NotImplementedException();
        }

        public bool ShouldFail(params T[] items)
        {
            throw new NotImplementedException();
        }

        public bool AnyIncluded(params T[] items)
        {
            throw new NotImplementedException();
        }

        public bool AnyExcluded(params T[] items)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
