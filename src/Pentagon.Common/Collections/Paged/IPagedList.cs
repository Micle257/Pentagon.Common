// -----------------------------------------------------------------------
//  <copyright file="IPagedList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections.Paged
{
    using System;

    public interface IPagedList
    {
        /// <summary> Gets the element count of the page. </summary>
        /// <value> The <see cref="int" />. </value>
        int PageSize { get; }

        /// <summary> Gets the page index. </summary>
        /// <value> The <see cref="Index" />. </value>
        Index PageIndex { get; }

        /// <summary> Gets the page number (one based). </summary>
        /// <value> The <see cref="int" />. </value>
        int PageNumber { get; }

        /// <summary> Gets the total count of elements in all pages. </summary>
        /// <value> The <see cref="int" />. </value>
        int TotalCount { get; }

        /// <summary> Gets the total count of pages. </summary>
        /// <value> The <see cref="int" />. </value>
        int TotalPages { get; }

        /// <summary> Gets a value indicating whether this page has previous page. </summary>
        /// <value> <c> true </c> if this page has previous page; otherwise, <c> false </c>. </value>
        bool HasPreviousPage { get; }

        /// <summary> Gets a value indicating whether this page has next page. </summary>
        /// <value> <c> true </c> if this page has next page; otherwise, <c> false </c>. </value>
        bool HasNextPage { get; }
    }
}