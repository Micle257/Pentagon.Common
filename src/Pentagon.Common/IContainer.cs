// -----------------------------------------------------------------------
//  <copyright file="IContainer.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System.Collections.Generic;

    /// <summary> Represents a collection of indexed elements with . </summary>
    /// <typeparam name="T"> The type of element. </typeparam>
    public interface IContainer<T> : IReadOnlyList<T>
    {
        /// <summary> Gets the index of value in this container. </summary>
        /// <value> The integer of index. </value>
        /// <param name="value"> The value to find the index of. </param>
        /// <returns> The found index value; otherwise negative one. </returns>
        int this[T value] { get; }

        /// <summary> Adds the item to the collection. </summary>
        /// <param name="item"> The item to add. </param>
        void AddItem(T item);
    }

    public interface INotifyContainer<T> : IContainer<T>, ICollectionNotify {
        
    }
}