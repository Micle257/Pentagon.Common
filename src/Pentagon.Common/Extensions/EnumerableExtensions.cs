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
    using System.Text;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="IEnumerable{T}" />. </summary>
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
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            var i = 0;
            var count = collection.Count;
            while (i < count && selector(i))
                i++;
            return i;
        }

        /// <summary> Dynamically shifts collection to the right. Last element becomes first. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An iteration of the shifted collection. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="collection" /> is <see langword="null" /> </exception>
        [NotNull]
        [Pure]
        public static IEnumerable<T> ShiftRight<T>([NotNull] this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            var list = collection.ToList();

            yield return list[list.Count - 1];
            for (var i = 0; i < list.Count - 1; i++)
                yield return list[i];
        }

        /// <summary> Dynamically shifts collection to the left. First element becomes last. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An iteration of the shifted collection. </returns>
        /// <exception cref="ArgumentNullException"> <paramref name="collection" /> is <see langword="null" /> </exception>
        [NotNull]
        [Pure]
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

        /// <summary> Changes collection so that it contains only the elements in one or the other collection not both. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="coll"> The collection. </param>
        /// <param name="second"> The second collection. </param>
        /// <returns> A collection with union of both unique items in collections. </returns>
        [Pure]
        public static IEnumerable<T> SymmetricExcept<T>([NotNull] this IEnumerable<T> coll, [NotNull] IEnumerable<T> second)
        {
            if (coll == null)
                throw new ArgumentNullException(nameof(coll));

            if (second == null)
                throw new ArgumentNullException(nameof(second));

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
        public static IEnumerable<T> Append<T>([NotNull] this IEnumerable<T> collection, [CanBeNull] IEnumerable<T> toAdd)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
                yield return item;

            if (toAdd == null)
                yield break;

            foreach (var item in toAdd)
                yield return item;
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
        [NotNull]
        [Pure]
        public static IEnumerable<T> Prepend<T>([NotNull] this IEnumerable<T> collection, [CanBeNull] IEnumerable<T> toAdd)
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
        [NotNull]
        [Pure]
        public static IEnumerable<T> Prepend<T>([NotNull] this IEnumerable<T> collection, params T[] toAdd) => Prepend(collection, toAdd.AsEnumerable());

        /// <summary> Groups the elements of a sequence into fixed-sized sub-sequences. </summary>
        /// <typeparam name="T"> The type of an item. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <param name="size"> The size of a group. </param>
        /// <returns> An iteration of grouped sub-sequences. </returns>
        /// <exception cref="ArgumentNullException"> collection </exception>
        /// <exception cref="ArgumentOutOfRangeException"> size - The size of group size must be non-zero positive integer. </exception>
        [NotNull]
        [Pure]
        public static IEnumerable<IEnumerable<T>> GroupBySize<T>([NotNull] this IEnumerable<T> collection, int size)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (size <= 0)
                throw new ArgumentOutOfRangeException(nameof(size), size, message: "The size of group size must be non-zero positive integer.");

            return collection
                   .Select((a, i) => new {Item = a, Index = i})
                   .GroupBy(a => a.Index / size)
                   .Select(a => a.Select(b => b.Item));
        }

        /// <summary> Sorts the elements of a sequence in ascending order. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted. </returns>
        /// <exception cref="ArgumentNullException"> collection </exception>
        [NotNull]
        [Pure]
        public static IOrderedEnumerable<T> OrderBySelf<T>([NotNull] this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection.OrderBy(a => a, Comparer<T>.Default);
        }

        /// <summary> Sorts the elements of a sequence in descending order. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An <see cref="IOrderedEnumerable{T}" /> whose elements are sorted. </returns>
        /// <exception cref="ArgumentNullException"> collection </exception>
        [NotNull]
        [Pure]
        public static IOrderedEnumerable<T> OrderBySelfDescending<T>([NotNull] this IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection.OrderByDescending(a => a, Comparer<T>.Default);
        }

        /// <summary> Filter null items from a sequence. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="collection"> The collection. </param>
        /// <returns> An <see cref="IEnumerable{T}" /> without null values. </returns>
        /// <exception cref="ArgumentNullException"> collection </exception>
        [NotNull]
        [ItemNotNull]
        [Pure]
        public static IEnumerable<T> WhereNotNull<T>([NotNull] this IEnumerable<T> collection)
                where T : class
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return collection.Where(a => a != null);
        }

        /// <summary> Converts the byte iteration to HEX string representation. </summary>
        /// <param name="bytes"> The bytes. </param>
        /// <returns> A <see cref="string" />. </returns>
        [NotNull]
        public static string ToHexString([NotNull] this IEnumerable<byte> bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            var sb = bytes.Select(a => a.ToString(format: "X2"))
                          .Aggregate(new StringBuilder(), (a, b) => a?.Append(value: " ").Append(b), a => a?.ToString().Trim());

            return sb ?? "";
        }
    }
}