// -----------------------------------------------------------------------
//  <copyright file="IFilterableCollection.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using JetBrains.Annotations;

    /// <summary> Represents a collection which can be filtered with <see cref="CollectionFilter{T}" />. </summary>
    /// <typeparam name="T"> The type of an element. </typeparam>
    public interface IFilterableCollection<T>
    {
        /// <summary> Gets the filter instance. </summary>
        /// <value> The <see cref="ICollectionFilter{T}" />. </value>
        [NotNull]
        ICollectionFilter<T> Filter { get; }

        /// <summary> Applies the filter to this collection. </summary>
        /// <param name="filter"> The filter. </param>
        void ApplyFilter([NotNull] ICollectionFilter<T> filter);
    }
}