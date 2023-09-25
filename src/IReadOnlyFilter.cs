using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter
{
    /// <summary>
    /// Interface for a filtering mechanism for collections of items of type T, readonly.
    /// <para>Keywords</para>
    /// <para>Explicitly - Registered manually using Include and Exclude methods, overriding the <see cref="Default"/> behaviour.</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyFilter<T>
    {
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

        /// <summary>
        /// Determines if the <paramref name="item"/> is contained in either <see cref="ExplicitIncludedItems"/> or <see cref="ExplicitExcludedItems"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ContainsExplicitly(T item);

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

        #endregion
    }
}
