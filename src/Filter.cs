using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    /// <summary>
    /// Provides a filtering mechanism for collections of items of type T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Filter<T> : IFilter<T>
        where T : notnull, IEquatable<T>
    {
        public FilterType Default { get; set; }

        private readonly HashSet<T> _include = new HashSet<T>();
        private readonly HashSet<T> _exclude = new HashSet<T>();

        public bool Equals(IFilter<T>? other)
        {
            if (other == null)
                return false;

            if (Default != other.Default)
                return false;

            foreach (var item in _include)
            {
                if (!other.ShouldInclude(item))
                    return false;
            }

            foreach (var item in _exclude)
            {
                if (other.ShouldInclude(item))
                    return false;
            }

            return true;
        }

        public void Exclude(T item)
            => _exclude.Add(item);

        public void Include(T item)
            => _include.Add(item);

        public bool ShouldInclude(T item)
        {
            if (_include.Contains(item))
                return true;
            if (_exclude.Contains(item))
                return false;
            return Default == FilterType.Include;
        }

        public void Clear()
        {
            _include.Clear();
            _exclude.Clear();
        }

        public object Clone()
        {
            var filter = new Filter<T>();

            foreach(var item in _include)
                filter.Include(item);

            foreach(var item in _exclude)
                filter.Exclude(item);

            return filter;
        }
    }
}
