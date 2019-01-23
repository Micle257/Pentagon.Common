// -----------------------------------------------------------------------
//  <copyright file="PageList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary> Represents a manager for managing paging for a collection. </summary>
    /// <typeparam name="TEntity"> The type of the element. </typeparam>
    public class PagedList<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagedList{T}" /> class.
        /// </summary>
        /// <param name="source">The source collection.</param>
        /// <param name="totalCount">The total count.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="pageIndex">Index of the page.</param>
        public PagedList(IEnumerable<TEntity> source, int totalCount, int pageSize, int pageIndex)
        {
            Items = source;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
            
            TotalPages = (int) (TotalCount / (double) PageSize) + 1;
        }
        
        /// <summary> Gets the element count of the page. </summary>
        /// <value> The <see cref="int" />. </value>
        public int PageSize { get; }
        
        /// <summary> Gets the page index. </summary>
        /// <value> The <see cref="int" />. </value>
        public int PageIndex { get; }
        
        /// <summary> Gets the page number (one based). </summary>
        /// <value> The <see cref="int" />. </value>
        public int PageNumber => PageIndex + 1;
        
        /// <summary> Gets the total count of elements. </summary>
        /// <value> The <see cref="int" />. </value>
        public int TotalCount { get; }
        
        /// <summary> Gets the total count of pages. </summary>
        /// <value> The <see cref="int" />. </value>
        public int TotalPages { get; }
        
        /// <summary> Gets the source collection. </summary>
        /// <value> The <see cref="IEnumerable{TEntity}" />. </value>
        public IEnumerable<TEntity> Items { get; }
        
        /// <summary> Gets a value indicating whether this page has previous page. </summary>
        /// <value>
        ///     <c> true </c> if this page has previous page; otherwise, <c> false </c>.
        /// </value>
        public bool HasPreviousPage => PageIndex != 0;
        
        /// <summary> Gets a value indicating whether this page has next page. </summary>
        /// <value>
        ///     <c> true </c> if this page has next page; otherwise, <c> false </c>.
        /// </value>
        public bool HasNextPage => PageNumber < TotalPages;
    }
}