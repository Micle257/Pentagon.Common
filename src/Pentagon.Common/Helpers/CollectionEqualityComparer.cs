// -----------------------------------------------------------------------
//  <copyright file="CollectionEqualityComparer.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CollectionEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
    {
        readonly bool _ignoreOrder;
        readonly IEqualityComparer<T> _comparer;

        public CollectionEqualityComparer(bool ignoreOrder = true, IEqualityComparer<T> comparer = null)
        {
            _ignoreOrder = ignoreOrder;
            _comparer = comparer ?? EqualityComparer<T>.Default;
        }

        #region IEquatable members

        public bool Equals(IEnumerable<T> first, IEnumerable<T> second)
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

        #endregion

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
            int firstNullCount;
            int secondNullCount;

            var firstElementCounts = GetElementCounts(first, out firstNullCount);
            var secondElementCounts = GetElementCounts(second, out secondNullCount);

            if (firstNullCount != secondNullCount || firstElementCounts.Count != secondElementCounts.Count)
                return true;

            foreach (var kvp in firstElementCounts)
            {
                var firstElementCount = kvp.Value;
                int secondElementCount;
                secondElementCounts.TryGetValue(kvp.Key, out secondElementCount);

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
                    int num;
                    dictionary.TryGetValue(element, out num);
                    num++;
                    dictionary[element] = num;
                }
            }

            return dictionary;
        }
    }
}