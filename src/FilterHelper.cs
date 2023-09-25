using Filter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter
{
    internal static class FilterHelper
    {
        public static bool Equals<T>(IFilter<T>? filter1, IFilter<T>? filter2)
            where T : IEquatable<T>
        {
            if (filter1 is null || filter2 is null)
                return false;

            if (object.ReferenceEquals(filter1, filter2))
                return true;

            if (!filter1.Default.Equals(filter2.Default))
                return false;

            if (!Enumerable.SequenceEqual(filter1.ExplicitExcludedItems, filter2.ExplicitExcludedItems))
                return false;

            if (!Enumerable.SequenceEqual(filter1.ExplicitIncludedItems, filter2.ExplicitIncludedItems))
                return false;

            return true;
        }
    }
}
