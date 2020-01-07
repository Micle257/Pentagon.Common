// -----------------------------------------------------------------------
//  <copyright file="HierarchyListItem.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections.Tree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    /// <summary> Represents an item in hierarchy structure. Also implements <see cref="IReadOnlyCollection{T}"/> for <see cref="Children"/>. </summary>
    /// <typeparam name="T"> The type of the inner value. </typeparam>
    public class HierarchyListNode<T> : IReadOnlyCollection<HierarchyListNode<T>>
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
            Id = Guid.NewGuid();
            Value = value;
            Parent = parent;
            Depth = depth;
        }

        public Guid Id { get; }

        /// <summary>Gets the depth in current hierarchy context.</summary>
        /// <value>The depth.</value>
        public int Depth { get; }

        /// <summary> Gets the children items. </summary>
        /// <value> The list of the <see cref="IList{T}" />. </value>
        [NotNull]
        public IReadOnlyList<HierarchyListNode<T>> Children => _children;

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
        public HierarchyListNode<T> AddChildren(T item)
        {
            var hierarchyListNode = new HierarchyListNode<T>(item, this, Depth + 1);

            _children.Add(hierarchyListNode);

            return hierarchyListNode;
        }

        /// <inheritdoc />
        public IEnumerator<HierarchyListNode<T>> GetEnumerator() => Children.GetEnumerator();

        /// <inheritdoc />
        public override string ToString() => $"Hierarchy node (Depth={Depth}): {Value}";

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int Count => Children.Count;
    }
}