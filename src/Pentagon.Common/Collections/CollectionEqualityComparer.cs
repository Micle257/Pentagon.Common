// -----------------------------------------------------------------------
//  <copyright file="CollectionEqualityComparer.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary>
    /// Provides equality comparer for <see cref="IEnumerable{T}"/>. Compares on count and item equality.
    /// </summary>
    /// <typeparam name="T">The type of element in <see cref="IEnumerable{T}"/>.</typeparam>
    public class CollectionEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        readonly bool _ignoreOrder;
        readonly IEqualityComparer<T> _comparer;

        public CollectionEqualityComparer(bool ignoreOrder = true, IEqualityComparer<T> comparer = null)
        {
            _ignoreOrder = ignoreOrder;
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public bool Equals([CanBeNull] IEnumerable<T> first, [CanBeNull] IEnumerable<T> second)
        {
            if (first == null)
                return second == null;

            if (second == null)
                return false;

            if (ReferenceEquals(first, second))
                return true;

            if (first is ICollection<T> firstCollection && second is ICollection<T> secondCollection)
            {
                if (firstCollection.Count != secondCollection.Count)
                    return false;

                if (firstCollection.Count == 0)
                    return true;
            }

            if (_ignoreOrder)
                return !HaveMismatchedElement(first, second);

            return first.SequenceEqual(second);
        }

        public int GetHashCode(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var hash = 17;

            foreach (var val in enumerable.OrderBy(x => x))
                hash = hash * 23 + (val?.GetHashCode() ?? 42);

            return hash;
        }

        bool HaveMismatchedElement(IEnumerable<T> first, IEnumerable<T> second)
        {
            var firstElementCounts = GetElementCounts(first, out var firstNullCount);
            var secondElementCounts = GetElementCounts(second, out var secondNullCount);

            if (firstNullCount != secondNullCount || firstElementCounts.Count != secondElementCounts.Count)
                return true;

            foreach (var value in firstElementCounts)
            {
                var firstElementCount = value.Value;
                secondElementCounts.TryGetValue(value.Key, out var secondElementCount);

                if (firstElementCount != secondElementCount)
                    return true;
            }

            return false;
        }

        Dictionary<T, int> GetElementCounts(IEnumerable<T> enumerable, out int nullCount)
        {
            var dictionary = new Dictionary<T, int>(_comparer);
            nullCount = 0;

            foreach (var element in enumerable)
            {
                if (element == null)
                    nullCount++;
                else
                {
                    dictionary.TryGetValue(element, out var number);
                    number++;
                    dictionary[element] = number;
                }
            }

            return dictionary;
        }
    }
}