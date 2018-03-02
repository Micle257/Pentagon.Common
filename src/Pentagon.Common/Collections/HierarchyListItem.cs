// -----------------------------------------------------------------------
//  <copyright file="HierarchyListItem.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary> Represents an item in hierarchy structure. </summary>
    /// <typeparam name="T"> </typeparam>
    public class HierarchyListItem<T>
    {
        /// <summary> Initializes a new instance of the <see cref="HierarchyListItem{T}" /> class. </summary>
        /// <param name="value"> The value. </param>
        /// <param name="parent"> The parent. </param>
        public HierarchyListItem(T value, HierarchyListItem<T> parent)
        {
            Value = value;
            Parent = parent;
        }

        /// <summary> Gets the parent. </summary>
        /// <value> The list of the <see cref="HierarchyListItem{T}" />. </value>
        public HierarchyListItem<T> Parent { get; }

        /// <summary> Gets the children items. </summary>
        /// <value> The list of the <see cref="IList{T}" />. </value>
        public IList<HierarchyListItem<T>> Children { get; } = new List<HierarchyListItem<T>>();

        /// <summary> Gets the item that share the same parent. </summary>
        /// <value> The list of the <see cref="HierarchyListItem{T}" />. </value>
        public IList<HierarchyListItem<T>> Siblings => GetSiblings();

        /// <summary> Gets a value indicating whether this item is root. </summary>
        /// <value> <c> true </c> if this item is root; otherwise, <c> false </c>. </value>
        public bool IsRoot => Parent == null;

        /// <summary> Gets or sets the value. </summary>
        /// <value> The <see cref="T" />. </value>
        public T Value { get; set; }

        /// <summary> Adds a children. </summary>
        /// <param name="item"> The children. </param>
        public void AddChildren(T item)
        {
            Children.Add(new HierarchyListItem<T>(item, this));
        }

        /// <summary> Gets the siblings. </summary>
        /// <returns> A list of the <see cref="HierarchyListItem{T}" />. </returns>
        IList<HierarchyListItem<T>> GetSiblings()
        {
            if (IsRoot)
                return null;

            if (Parent.Children != null && Parent.Children.Count != 0)
                return Parent.Children.Except(new[] {this}).ToList();

            return null;
        }
    }
}