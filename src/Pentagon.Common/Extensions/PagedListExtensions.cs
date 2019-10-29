// -----------------------------------------------------------------------
//  <copyright file="PagedListExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Linq;
    using Collections;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="PagedList{TEntity}" />. </summary>
    public static class PagedListExtensions
    {
        /// <summary> Projects each element of a sequence into a new form. </summary>
        /// <typeparam name="T"> The type of the elements of source. </typeparam>
        /// <typeparam name="TCast"> The type of the value returned by selector. </typeparam>
        /// <param name="list"> A <see cref="PagedList{TEntity}" /> of values to invoke a transform function on. </param>
        /// <param name="callback"> A transform function to apply to each element. </param>
        /// <returns> An iteration whose elements are the result of invoking the transform function on each element of source. </returns>
        /// <exception cref="System.ArgumentNullException"> list or callback </exception>
        [NotNull]
        [ItemNotNull]
        public static PagedList<TCast> Select<T, TCast>([NotNull] this PagedList<T> list, [NotNull] Func<T, TCast> callback)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            return new PagedList<TCast>(list.Select(selector: callback), totalCount: list.TotalCount, pageSize: list.PageSize, pageIndex: list.PageIndex);
        }
    }
}