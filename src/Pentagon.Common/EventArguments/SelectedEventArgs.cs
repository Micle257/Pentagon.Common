// -----------------------------------------------------------------------
//  <copyright file="SelectedEventArgs.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.EventArguments
{
    using System;

    /// <summary> Provides data for <see cref="SelectedEventHandler{T}" /> method. </summary>
    /// <typeparam name="T"> The type of the element. </typeparam>
    public sealed class SelectedEventArgs<T> : EventArgs
    {
        /// <summary> Initializes a new instance of the <see cref="SelectedEventArgs{T}" /> class. </summary>
        /// <param name="selectedItem"> The selected item. </param>
        /// <param name="index"> The index of selected item. </param>
        public SelectedEventArgs(T selectedItem, int index)
        {
            SelectedItem = selectedItem;
            Index = index;
        }

        /// <summary> Gets the selected item. </summary>
        /// <value> The <see cref="T" />. </value>
        public T SelectedItem { get; }

        /// <summary> Gets the index of selected item. </summary>
        /// <value> The <see cref="int" />. </value>
        public int Index { get; }
    }
}