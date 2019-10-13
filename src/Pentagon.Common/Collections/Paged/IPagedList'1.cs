// -----------------------------------------------------------------------
//  <copyright file="IPagedList'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using System.Collections.Generic;

    public interface IPagedList<out TEntity> : IPagedList, IEnumerable<TEntity>
    {
        /// <summary> Gets the page index range. </summary>
        /// <value> The <see cref="Range" />. </value>
        Range ItemRange { get; }

        /// <summary> Gets the number of elements in this page. </summary>
        /// <returns> The <see cref="int" />. </returns>
        int Count { get; }

        /// <summary> Gets a value indicating whether this page has reach its page size. </summary>
        /// <value> <c> true </c> if this page is full; otherwise, <c> false </c>. </value>
        bool IsPageFull { get; }
    }
}