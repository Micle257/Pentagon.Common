// -----------------------------------------------------------------------
//  <copyright file="PaginationHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Collections;
    using JetBrains.Annotations;

    /// <summary> Provides helper logic for pagination. </summary>
    public static class PaginationHelper
    {
        /// <summary> Creates all valid pages from source sequence. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="source"> The source. </param>
        /// <param name="pageSize"> Size of the page. </param>
        /// <returns> A read-only list of <see cref="PagedList{TEntity}" /> shaped by source sequence and page size. </returns>
        /// <exception cref="ArgumentNullException"> source </exception>
        [Pure]
        public static IReadOnlyList<PagedList<T>> CreateAllPages<T>([NotNull] IEnumerable<T> source, int pageSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var sourceList = source as List<T> ?? source.ToList();

            var pageCount = (int) (sourceList.Count / (double) pageSize) + 1;

            var pages = new List<PagedList<T>>();

            for (var i = 0; i < pageCount; i++)
                pages.Add(CreatePagedList(sourceList, i + 1, pageSize));

            return pages;
        }

        /// <summary> Creates the paged list from source sequence, if page cannot be created returns null. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="source"> The source sequence. </param>
        /// <param name="pageNumber"> The page number. </param>
        /// <param name="pageSize"> Size of the page. </param>
        /// <returns> A <see cref="PagedList{TEntity}" /> shaped by source sequence and parameters. </returns>
        /// <exception cref="ArgumentNullException"> source </exception>
        [Pure]
        public static PagedList<T> CreatePagedList<T>([NotNull] IEnumerable<T> source, int pageNumber, int pageSize)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var sourceList = source as List<T> ?? source.ToList();

            if (pageSize <= 0 || pageNumber <= 0)
                return null;

            if (sourceList.Count <= (pageNumber - 1) * pageSize)
                return null;

            var pageData = sourceList
                           .Skip((pageNumber - 1) * pageSize)
                           .Take(pageSize);

            var pagedList = new PagedList<T>(pageData, sourceList.Count, pageSize, pageNumber - 1);

            return pagedList;
        }
    }
}