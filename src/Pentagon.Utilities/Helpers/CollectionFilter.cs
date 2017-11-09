// -----------------------------------------------------------------------
//  <copyright file="CollectionFilter.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using JetBrains.Annotations;

    /// <summary> Represents a filter for a collection, that can ensure public items exposure by the rule predicate. </summary>
    /// <typeparam name="T"> The type of an element. </typeparam>
    public class CollectionFilter<T>
    {
        /// <summary> Initializes a new instance of the <see cref="CollectionFilter{T}" /> class with filter predicate. </summary>
        /// <param name="rulePredicate"> The rule predicate. </param>
        public CollectionFilter(Predicate<T> rulePredicate = null)
        {
            Predicate = rulePredicate ?? (obj => true);
            IsAllPass = true;
        }

        /// <summary> Gets the predicate/rule for this filter. </summary>
        /// <value> The method for determining filtered elements. </value>
        [NotNull]
        public Predicate<T> Predicate { get; }

        /// <summary> Gets a value indicating whether this filter pass every element. </summary>
        /// <value> <c> true </c> if this instance is all pass; otherwise, <c> false </c>. </value>
        public bool IsAllPass { get; }

        /// <summary> Applies this filter to the filterable collection. </summary>
        /// <param name="coll"> The collection. </param>
        public void ApplyFilter(IFilterableCollection<T> coll)
        {
            coll?.ApplyFilter(this);
        }
    }
}