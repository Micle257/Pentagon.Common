// -----------------------------------------------------------------------
//  <copyright file="PagedList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Represents a manager for managing paging for a collection. </summary>
    /// <typeparam name="TEntity"> The type of the element. </typeparam>
    public class PagedList<TEntity> : IReadOnlyList<TEntity>
    {
        /// <summary> The source collection. </summary>
        [NotNull]
        readonly List<TEntity> _items;

        /// <summary> Initializes a new instance of the <see cref="PagedList{T}" /> class. </summary>
        /// <param name="source"> The source collection. </param>
        /// <param name="totalCount"> The total count. </param>
        /// <param name="pageSize"> Size of the page. </param>
        /// <param name="pageIndex"> Index of the page. </param>
        public PagedList([NotNull] IEnumerable<TEntity> source, int? totalCount, int? pageSize, Index? pageIndex)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            _items = source.ToList();
            TotalCount = totalCount ?? _items.Count;
            PageSize = pageSize ?? _items.Count;
            PageIndex = pageIndex ?? 0;

            var totalPages = TotalCount / (double)PageSize;
            var isRemainder = TotalCount % PageSize != 0;

            if (isRemainder)
                TotalPages = (int)totalPages + 1;
            else
                TotalPages = (int)totalPages;
        }

        /// <summary> Gets the element count of the page. </summary>
        /// <value> The <see cref="int" />. </value>
        public int PageSize { get; }

        /// <summary> Gets the page index. </summary>
        /// <value> The <see cref="Index" />. </value>
        public Index PageIndex { get; }

        /// <summary> Gets the page number (one based). </summary>
        /// <value> The <see cref="int" />. </value>
        public int PageNumber => PageIndex.Value + 1;

        /// <summary> Gets the total count of elements. </summary>
        /// <value> The <see cref="int" />. </value>
        public int TotalCount { get; }

        /// <summary> Gets the total count of pages. </summary>
        /// <value> The <see cref="int" />. </value>
        public int TotalPages { get; }

        /// <summary> Gets the page index range. </summary>
        /// <value> The <see cref="Range" />. </value>
        public Range ItemRange
        {
            get
            {
                var start = PageIndex.Value * PageSize;
                var end = start + Count - 1;

                return start..end;
            }
        }

        /// <summary> Gets a value indicating whether this page has previous page. </summary>
        /// <value> <c> true </c> if this page has previous page; otherwise, <c> false </c>. </value>
        public bool HasPreviousPage => PageIndex.Value != 0;

        /// <summary> Gets a value indicating whether this page has next page. </summary>
        /// <value> <c> true </c> if this page has next page; otherwise, <c> false </c>. </value>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <inheritdoc />
        public int Count => _items.Count;

        /// <inheritdoc />
        public TEntity this[int index] => _items[index];

        /// <inheritdoc />
        public IEnumerator<TEntity> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}