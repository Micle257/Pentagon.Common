// -----------------------------------------------------------------------
//  <copyright file="SelectableHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using JetBrains.Annotations;

    /// <summary> Provides helper methods for select logic of <see cref="ISelectable{T}" />. </summary>
    public static class SelectableHelper
    {
        /// <summary> Selects the previous element of the selectable container. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="selectable"> The container. </param>
        public static void Previous<T>([NotNull] ISelectable<T> selectable)
            where T : class, IContainable
        {
            if (selectable.Current == null)
                throw new ArgumentNullException(nameof(selectable.Current));
            if (selectable.Count <= 1)
                return;
            selectable.Select(selectable.Current.Index == selectable.Count - 1 ? selectable[0] : selectable[selectable.Current.Index + 1]);
        }

        /// <summary> Selects the next element of the selectable container. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="selectable"> The container. </param>
        public static void Next<T>([NotNull] ISelectable<T> selectable)
            where T : class, IContainable
        {
            if (selectable.Current == null)
                throw new ArgumentNullException(nameof(selectable.Current));
            if (selectable.Count <= 1)
                return;
            selectable.Select(selectable.Current.Index == 0 ? selectable[selectable.Count - 1] : selectable[selectable.Current.Index - 1]);
        }
    }
}