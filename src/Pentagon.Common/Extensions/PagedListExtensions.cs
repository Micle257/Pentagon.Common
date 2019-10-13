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
    using Collections.Paged;
    using JetBrains.Annotations;

    public static class PagedListExtensions
    {
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