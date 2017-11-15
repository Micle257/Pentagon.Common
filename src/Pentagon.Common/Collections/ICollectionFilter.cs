namespace Pentagon.Collections {
    using System;

    /// <summary> Represents a filter for a collection, that can ensure public items exposure by the rule predicate. </summary>
    /// <typeparam name="T"> The type of an element. </typeparam>
    public interface ICollectionFilter<T> {
        /// <summary> Gets the predicate/rule for this filter. </summary>
        /// <value> The method for determining filtered elements. </value>
        Predicate<T> Predicate { get; }

        /// <summary> Gets a value indicating whether this filter pass every element. </summary>
        /// <value> <c> true </c> if this instance is all pass; otherwise, <c> false </c>. </value>
        bool IsAllPass { get; }

        /// <summary> Applies this filter to the filterable collection. </summary>
        /// <param name="coll"> The collection. </param>
        void ApplyFilter(IFilterableCollection<T> coll);
    }
}