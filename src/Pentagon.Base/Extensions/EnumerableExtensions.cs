// -----------------------------------------------------------------------
//  <copyright file="EnumerableExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="Enumerable" />. </summary>
    public static class EnumerableExtensions
    {
        /// <summary> Finds the index where to insert value by given selector. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <param name="selector"> The predicate selector. </param>
        /// <returns> An <see cref="int" /> of index. </returns>
        public static int FindOrderIndex<T>([NotNull] this ICollection<T> collection, [NotNull] Func<int, bool> selector)
        {
            Require.NotNull(() => collection);
            Require.NotNull(() => selector);
            var i = 0;
            var count = collection.Count;
            while (i < count && selector(i))
                i++;
            return i;
        }

        /// <summary> Dynamically shifts collection to the right. </summary>
        /// <typeparam name="T"> Type of collection items. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="collection" /> is <see langword="null" /> </exception>
        [NotNull]
        public static IEnumerable<T> ShiftRight<T>([NotNull] this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            var list = collection.ToList();
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            yield return list[list.Count - 1];
            for (var i = 0; i < list.Count - 1; i++)
                yield return list[i];
        }

        /// <summary> Dynamically shifts collection to the left. </summary>
        /// <typeparam name="T"> Type of collection items. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <exception cref="ArgumentNullException"> <paramref name="collection" /> is <see langword="null" /> </exception>
        [NotNull]
        public static IEnumerable<T> ShiftLeft<T>([NotNull] this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            var a = false;
            var firstElement = default(T);
            foreach (var e in collection)
            {
                if (a)
                    yield return e;
                else
                    firstElement = e;
                a = true;
            }
            yield return firstElement;
        }

        /// <summary> Changes collection so that it contains only the elements in one or the other collection — not both. </summary>
        /// <typeparam name="T"> Type of collection's items. </typeparam>
        /// <param name="coll"> The collection. </param>
        /// <param name="second"> The second collection. </param>
        /// <returns> A collection with union of both unique items in collections. </returns>
        public static IEnumerable<T> SymmetricExcept<T>([NotNull] this IEnumerable<T> coll, [NotNull] IEnumerable<T> second)
        {
            var hash = new HashSet<T>(coll);
            hash.SymmetricExceptWith(second);
            return hash;
        }
    }
}