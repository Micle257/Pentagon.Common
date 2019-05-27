// -----------------------------------------------------------------------
//  <copyright file="HierarchyListItem.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Represents an item in hierarchy structure. </summary>
    /// <typeparam name="T"> The type of the inner value. </typeparam>
    public class HierarchyListNode<T>
    {
        /// <summary> The list of children. </summary>
        [NotNull]
        readonly List<HierarchyListNode<T>> _children = new List<HierarchyListNode<T>>();

        /// <summary> Initializes a new instance of the <see cref="HierarchyListNode{T}" /> class. </summary>
        /// <param name="value"> The value. </param>
        /// <param name="parent"> The parent. </param>
        /// <param name="depth"> The depth of a node. </param>
        internal HierarchyListNode(T value, HierarchyListNode<T> parent, int depth)
        {
            Value = value;
            Parent = parent;
            Depth = depth;
        }

        /// <summary>Gets the depth in current hierarchy context.</summary>
        /// <value>The depth.</value>
        public int Depth { get; }

        /// <summary> Gets the children items. </summary>
        /// <value> The list of the <see cref="IList{T}" />. </value>
        [NotNull]
        public IReadOnlyList<HierarchyListNode<T>> Children => _children;

        /// <summary> Gets the item that share the same parent. </summary>
        /// <value> The list of the <see cref="HierarchyListNode{T}" />. </value>
        [NotNull]
        public IReadOnlyList<HierarchyListNode<T>> Siblings => GetSiblings().ToList();

        /// <summary> Gets a value indicating whether this item is root. </summary>
        /// <value> <c> true </c> if this item is root; otherwise, <c> false </c>. </value>
        public bool IsRoot => Parent == null;

        /// <summary> Gets the parent. </summary>
        /// <value> The list of the <see cref="HierarchyListNode{T}" />. </value>
        [CanBeNull]
        public HierarchyListNode<T> Parent { get; }

        /// <summary> Gets or sets the value. </summary>
        /// <value> The <see cref="T" />. </value>
        public T Value { get; set; }

        /// <summary> Adds a children. </summary>
        /// <param name="item"> The children. </param>
        public void AddChildren(T item)
        {
            _children.Add(new HierarchyListNode<T>(item, this, Depth + 1));
        }

        /// <summary>
        /// Gets all children nodes.
        /// </summary>
        /// <returns>Read-only list of <see cref="HierarchyListNode{T}"/>.</returns>
        public IReadOnlyList<HierarchyListNode<T>> GetAllChildren()
        {
            var result = new List<HierarchyListNode<T>>();
            var cursor = Children;

            while (cursor.Any())
            {
                result.AddRange(cursor);
                cursor = cursor.SelectMany(a => a.Children).ToList();
            }

            return result;
        }

        /// <summary> Gets the siblings. </summary>
        /// <returns> A list of the <see cref="HierarchyListNode{T}" />. </returns>
        [NotNull]
        [Pure]
        IEnumerable<HierarchyListNode<T>> GetSiblings()
        {
            if (IsRoot || Parent == null)
                yield break;

            if (Parent.Children.Count == 0)
                yield break;

            foreach (var node in Parent.Children.Except(new[] {this}))
                yield return node;
        }
    }
}