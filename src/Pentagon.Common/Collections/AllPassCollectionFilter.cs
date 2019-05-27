// -----------------------------------------------------------------------
//  <copyright file="AllPassCollectionFilter.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using JetBrains.Annotations;

    /// <summary> Represents an all pass collection filter. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public class AllPassCollectionFilter<T> : ICollectionFilter<T>
    {
        /// <summary> Initializes a new instance of the <see cref="AllPassCollectionFilter{T}" /> class. </summary>
        public AllPassCollectionFilter()
        {
            Predicate = obj => true;
        }

        /// <inheritdoc />
        public Predicate<T> Predicate { get;  }

        /// <inheritdoc />
        public void ApplyFilter([NotNull] IFilterableCollection<T> coll)
        {
            if (coll == null)
                throw new ArgumentNullException(nameof(coll));

            coll.ApplyFilter(this);
        }
    }
}