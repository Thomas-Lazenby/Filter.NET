using System;
using System.Collections.ObjectModel;

namespace Filter
{
    public class ReadOnlyFilter<T> : IFilter<T>
        where T : notnull, IEquatable<T>
    {
        private readonly IFilter<T> _filter;

        public ReadOnlyFilter(IFilter<T> filter)
        {
            _filter = filter;
        }

        public FilterType Default { get => _filter.Default; set => throw new NotSupportedException(nameof(Default)); }

        public IEnumerable<T> ExplicitIncludedItems => _filter.ExplicitIncludedItems;

        public IEnumerable<T> ExplicitExcludedItems => _filter.ExplicitExcludedItems;

        public bool AnyExcluded(params T[] items)
            => _filter.AnyExcluded(items);

        public bool AnyExplicitExcluded(params T[] items)
            => _filter.AnyExplicitExcluded(items);

        public bool AnyExplicitIncluded(params T[] items)
            => _filter.AnyExplicitIncluded(items);

        public bool AnyIncluded(params T[] items)
            => _filter.AnyIncluded(items);

        public IFilter<T> Clear()
            => throw new NotSupportedException(nameof(Clear));

        public object Clone()
            => throw new NotSupportedException(nameof(Clone));

        public bool ContainsExplicitly(T item)
            => _filter.ContainsExplicitly(item);

        public bool Equals(IFilter<T>? other)
            => _filter.Equals(other);

        public IFilter<T> Exclude(T item)
            => throw new NotSupportedException(nameof(Exclude));

        public IFilter<T> Exclude(params T[] items)
            => throw new NotSupportedException(nameof(Exclude));

        public IFilter<T> Include(T item)
            => throw new NotSupportedException(nameof(Include));

        public IFilter<T> Include(params T[] items)
            => throw new NotSupportedException(nameof(Include));

        public bool IsExcluded(T item)
            => _filter.IsExcluded(item);

        public bool IsExplicitlyExcluded(T item)
            => _filter.IsExplicitlyExcluded(item);

        public bool IsExplicitlyIncluded(T item)
            => _filter.IsExplicitlyIncluded(item);

        public bool IsIncluded(T item)
            => _filter.IsIncluded(item);

        public IFilter<T> SetAsDefault(T item)
            => throw new NotSupportedException(nameof(SetAsDefault));
    }
}

