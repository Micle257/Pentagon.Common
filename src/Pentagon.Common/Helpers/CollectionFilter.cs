// -----------------------------------------------------------------------
//  <copyright file="CollectionFilter.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;

    /// <summary> Represents a collection filter implementation. </summary>
    /// <typeparam name="T"> The type of an element. </typeparam>
    public class CollectionFilter<T> : ICollectionFilter<T>
    {
        /// <summary> Initializes a new instance of the <see cref="CollectionFilter{T}" /> class with filter predicate. </summary>
        /// <param name="rulePredicate"> The rule predicate. </param>
        public CollectionFilter(Predicate<T> rulePredicate = null)
        {
            Predicate = rulePredicate ?? (obj => true);
        }

        /// <inheritdoc />
        public Predicate<T> Predicate { get; }

        /// <inheritdoc />
        public void ApplyFilter(IFilterableCollection<T> coll)
        {
            coll?.ApplyFilter(this);
        }
    }

    public class AllPassCollectionFilter<T> : ICollectionFilter<T> {
        public AllPassCollectionFilter() {
            Predicate = obj => true;
        }

        /// <inheritdoc />
        public Predicate<T> Predicate { get; }

        /// <inheritdoc />
        public void ApplyFilter(IFilterableCollection<T> coll)
        {
            coll?.ApplyFilter(this);
        }
    }
}