// -----------------------------------------------------------------------
//  <copyright file="ICollectionFilter.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;

    /// <summary> Represents a filter for a collection, that can ensure public items exposure by the rule predicate. </summary>
    /// <typeparam name="T"> The type of an element. </typeparam>
    public interface ICollectionFilter<T>
    {
        /// <summary> Gets the predicate/rule for this filter. </summary>
        /// <value> The method for determining filtered elements. </value>
        Predicate<T> Predicate { get; }

        /// <summary> Applies this filter to the filterable collection. </summary>
        /// <param name="coll"> The collection. </param>
        void ApplyFilter(IFilterableCollection<T> coll);
    }
}