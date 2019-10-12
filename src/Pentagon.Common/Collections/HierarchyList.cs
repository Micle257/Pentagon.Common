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
    using System.Text;

    /// <summary> Represents a collection in hierarchy structure. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public class HierarchyList<T> : IEnumerable<HierarchyListNode<T>>
    {
        /// <summary> Initializes a new instance of the <see cref="HierarchyList{T}" /> class. </summary>
        /// <param name="rootItem"> The root item. </param>
        public HierarchyList(T rootItem)
        {
            Root = new HierarchyListNode<T>(rootItem, null, 0);
        }

        /// <summary> Gets the root item. </summary>
        /// <value> The list of the <see cref="HierarchyListNode{T}" />. </value>
        public HierarchyListNode<T> Root { get; }

        /// <inheritdoc />
        public IEnumerator<HierarchyListNode<T>> GetEnumerator() => new Enumerator(Root);

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public string ToTreeString()
        {
            var sb = new StringBuilder();

            foreach (var item in this)
            {
                sb.Append(new string('-', item.Depth));
                sb.Append(item.Value);
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary> Represents an enumerator for <see cref="HierarchyList{T}" />. </summary>
        class Enumerator : IEnumerator<HierarchyListNode<T>>
        {
            /// <summary> The root item. </summary>
            readonly HierarchyListNode<T> _node;

            /// <summary> The current item row. </summary>
            List<HierarchyListNode<T>> _currentItemRow;

            /// <summary> The current index in the row. </summary>
            int _currentIndex;

            /// <summary> Initializes a new instance of the <see cref="Enumerator" /> class. </summary>
            /// <param name="node"> The item. </param>
            public Enumerator(HierarchyListNode<T> node)
            {
                _node = node;
            }

            /// <inheritdoc />
            public HierarchyListNode<T> Current { get; private set; }

            /// <inheritdoc />
            object IEnumerator.Current => Current;

            /// <inheritdoc />
            public bool MoveNext()
            {
                if (_currentItemRow == null)
                    _currentItemRow = new List<HierarchyListNode<T>> {_node};
                else if (_currentItemRow.Count == 0)
                    return false;

                Current = _currentItemRow[_currentIndex] ?? throw new ArgumentNullException();

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