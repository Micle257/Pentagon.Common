// -----------------------------------------------------------------------
//  <copyright file="IPagedList'1.cs">
//   Copyright (c) Michal Pokorn�. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using System.Collections.Generic;
    using Ranges;

    /// <summary> Represents a paged list with elements. </summary>
    /// <typeparam name="TEntity"> The type of the element. </typeparam>
    public interface IPagedList<out TEntity> : IPagedList, IEnumerable<TEntity>
    {
        /// <summary> Gets the page index range. </summary>
        /// <value> The <see cref="Range{T}" />. </value>
        Range<int> ItemRange { get; }

        /// <summary> Gets the number of elements in this page. </summary>
        /// <returns> The <see cref="int" />. </returns>
        int Count { get; }

        /// <summary> Gets a value indicating whether this page has reach its page size. </summary>
        /// <value> <c> true </c> if this page is full; otherwise, <c> false </c>. </value>
        bool IsPageFull { get; }
    }
}