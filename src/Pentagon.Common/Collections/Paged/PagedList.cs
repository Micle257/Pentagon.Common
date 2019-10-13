// -----------------------------------------------------------------------
//  <copyright file="PagedList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections.Paged
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Represents a paged list defined as <see cref="IReadOnlyList{T}"/>. </summary>
    /// <typeparam name="TEntity"> The type of the element. </typeparam>
    public class PagedList<TEntity> : IPagedList<TEntity>, IReadOnlyList<TEntity>
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

            _items     = source.ToList();
            TotalCount = totalCount ?? _items.Count;
            PageSize   = pageSize ?? _items.Count;
            PageIndex  = pageIndex ?? 0;

            var totalPages  = TotalCount / (double) PageSize;
            var isRemainder = TotalCount % PageSize != 0;

            if (isRemainder)
                TotalPages = (int) totalPages + 1;
            else
                TotalPages = (int) totalPages;
        }

        /// <inheritdoc />
        public int PageSize { get; }

        /// <inheritdoc />
        public Index PageIndex { get; }

        /// <inheritdoc />
        public int PageNumber => PageIndex.Value + 1;

        /// <inheritdoc />
        public int TotalCount { get; }

        /// <inheritdoc />
        public int TotalPages { get; }

        /// <inheritdoc />
        public bool HasPreviousPage => PageIndex.Value != 0;

        /// <inheritdoc />
        public bool HasNextPage => PageNumber < TotalPages;

        /// <inheritdoc />
        public Range ItemRange
        {
            get
            {
                var start = PageIndex.Value * PageSize;
                var end   = (start + Count) - 1;

                return start..end;
            }
        }

        /// <inheritdoc />
        public int Count => _items.Count;

        /// <inheritdoc />
        public bool IsPageFull => Count == PageSize;

        /// <inheritdoc />
        public TEntity this[int index] => _items[index: index];

        /// <inheritdoc />
        public IEnumerator<TEntity> GetEnumerator() => _items.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary> Creates <see cref="PagedList{TEntity}" /> without data. Used for computation of pagination parameters. </summary>
        /// <param name="sourceCount"> The source count. </param>
        /// <param name="totalCount"> The total count. </param>
        /// <param name="pageSize"> Size of the page. </param>
        /// <param name="pageIndex"> Index of the page. </param>
        /// <returns> The <see cref="PagedList{TEntity}" /> with all elements of value <c> null </c>. </returns>
        [NotNull]
        [Pure]
        public static PagedList<TEntity> CreateBlank(int sourceCount, int? totalCount, int? pageSize, Index? pageIndex) =>
                new PagedList<TEntity>(Enumerable.Repeat<TEntity>(default, count: sourceCount), totalCount: totalCount, pageSize: pageSize, pageIndex: pageIndex);

        /// <summary> Creates <see cref="PagedList{TEntity}" /> without data. Used for computation of pagination parameters. </summary>
        /// <param name="totalCount"> The total count. </param>
        /// <param name="pageSize"> Size of the page. </param>
        /// <returns> The <see cref="PagedList{TEntity}" /> with all elements of value <c> null </c>. </returns>
        [NotNull]
        [Pure]
        public static PagedList<TEntity> CreateBlank(int totalCount, int pageSize) =>
                new PagedList<TEntity>(Array.Empty<TEntity>(), totalCount: totalCount, pageSize: pageSize, 0);
    }
}