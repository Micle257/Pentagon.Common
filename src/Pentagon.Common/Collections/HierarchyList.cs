// -----------------------------------------------------------------------
//  <copyright file="HierarchyList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> Represents a collection in hierarchy structure. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public class HierarchyList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchyList{T}"/> class.
        /// </summary>
        /// <param name="rootItem">The root item.</param>
        public HierarchyList(T rootItem)
        {
            Root = new HierarchyListItem<T>(rootItem, null);
        }

        /// <summary> Gets the root item. </summary>
        /// <value> The list of the <see cref="HierarchyListItem{T}" />. </value>
        public HierarchyListItem<T> Root { get;  }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => new Enumerator(Root);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary> Represents an enumerator for <see cref="HierarchyList{T}" />. </summary>
        class Enumerator : IEnumerator<T>
        {
            /// <summary>
            /// The root item.
            /// </summary>
            readonly HierarchyListItem<T> _item;

            /// <summary>
            /// The current item row.
            /// </summary>
            List<HierarchyListItem<T>> _currentItemRow;

            /// <summary>
            /// The current index in the row.
            /// </summary>
            int _currentIndex;

            /// <summary> Initializes a new instance of the <see cref="Enumerator" /> class. </summary>
            /// <param name="item"> The item. </param>
            public Enumerator(HierarchyListItem<T> item)
            {
                _item = item;
            }

            /// <inheritdoc />
            public T Current { get; private set; }

            /// <inheritdoc />
            object IEnumerator.Current => Current;

            /// <inheritdoc />
            public bool MoveNext()
            {
                if (_currentItemRow == null)
                    _currentItemRow = new List<HierarchyListItem<T>> {_item};
                else if (_currentItemRow.Count == 0)
                    return false;

                Current = (_currentItemRow[_currentIndex] ?? throw new ArgumentNullException()).Value;

                if (_currentIndex == _currentItemRow.Count - 1)
                {
                    _currentIndex = 0;
                    _currentItemRow = _currentItemRow.SelectMany(a => a.Children).ToList();
                }
                else
                    _currentIndex++;

                return true;
            }

            /// <inheritdoc />
            public void Reset()
            {
                Current = default;
            }

            /// <inheritdoc />
            public void Dispose() { }
        }
    }
}