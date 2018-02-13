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
        [Pure]
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
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An iteration of the shifted collection. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="collection" /> is <see langword="null" /> </exception>
        [NotNull]
        [Pure]
        public static IEnumerable<T> ShiftRight<T>([NotNull] this IEnumerable<T> collection)
        {
            Require.NotNull(() => collection);

            var list = collection.ToList();

            yield return list[list.Count - 1];
            for (var i = 0; i < list.Count - 1; i++)
                yield return list[i];
        }

        /// <summary> Dynamically shifts collection to the left. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An iteration of the shifted collection. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="collection" /> is <see langword="null" /> </exception>
        [NotNull]
        [Pure]
        public static IEnumerable<T> ShiftLeft<T>([NotNull] this IEnumerable<T> collection)
        {
            Require.NotNull(() => collection);

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
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="coll"> The collection. </param>
        /// <param name="second"> The second collection. </param>
        /// <returns> A collection with union of both unique items in collections. </returns>
        [Pure]
        public static IEnumerable<T> SymmetricExcept<T>([NotNull] this IEnumerable<T> coll, [NotNull] IEnumerable<T> second)
        {
            Require.NotNull(() => coll);
            Require.NotNull(() => second);

            var hash = new HashSet<T>(coll);
            hash.SymmetricExceptWith(second);
            return hash;
        }

        /// <summary> Appends items of the collection at the end of this collection. </summary>
        /// <typeparam name="T"> The type of the item. </typeparam>
        /// <param name="collection"> This collection. </param>
        /// <param name="toAdd"> To collection to append. </param>
        /// <returns> An iteration of appended collection. </returns>
        [Pure]
        public static IEnumerable<T> Append<T>([NotNull] this IEnumerable<T> collection, IEnumerable<T> toAdd)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
                yield return item;

            if (toAdd != null)
            {
                foreach (var item in toAdd)
                    yield return item;
            }
        }

        /// <summary> Appends items of the collection at the end of this collection. </summary>
        /// <typeparam name="T"> The type of the item. </typeparam>
        /// <param name="collection"> This collection. </param>
        /// <param name="toAdd"> To collection to append. </param>
        /// <returns> An iteration of appended collection. </returns>
        [Pure]
        public static IEnumerable<T> Append<T>([NotNull] this IEnumerable<T> collection, params T[] toAdd) => Append(collection, toAdd.AsEnumerable());

        /// <summary> Prepends items of the collection at the beginning of this collection. </summary>
        /// <typeparam name="T"> The type of the item. </typeparam>
        /// <param name="collection"> This collection. </param>
        /// <param name="toAdd"> To collection to prepend. </param>
        /// <returns> An iteration of prepended collection. </returns>
        [Pure]
        public static IEnumerable<T> Prepend<T>([NotNull] this IEnumerable<T> collection, IEnumerable<T> toAdd)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (toAdd != null)
            {
                foreach (var item in toAdd)
                    yield return item;
            }

            foreach (var item in collection)
                yield return item;
        }

        /// <summary> Prepends items of the collection at the beginning of this collection. </summary>
        /// <typeparam name="T"> The type of the item. </typeparam>
        /// <param name="collection"> This collection. </param>
        /// <param name="toAdd"> To collection to prepend. </param>
        /// <returns> An iteration of prepended collection. </returns>
        [Pure]
        public static IEnumerable<T> Prepend<T>([NotNull] this IEnumerable<T> collection, params T[] toAdd) => Prepend(collection, toAdd.AsEnumerable());

        /// <summary> Removes and returns the object at the beginning; and adds an object to the end of the <see cref="Queue{T}" />. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="queue"> The queue. </param>
        /// <param name="value"> The value to enqueue. </param>
        /// <returns> The <see cref="T" /> that represents dequeued value from the queue. </returns>
        public static T Requeue<T>([NotNull] this Queue<T> queue, T value)
        {
            Require.NotNull(() => queue);
            Require.NotEmpty(() => queue);

            var dequeuedValue = queue.Dequeue();
            queue.Enqueue(value);

            return dequeuedValue;
        }
    }
}