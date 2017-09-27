// -----------------------------------------------------------------------
//  <copyright file="IContainable.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    /// <summary> Represents an element in <see cref="IContainer{T}" /> with container dependency. </summary>
    public interface IContainable
    {
        /// <summary> Gets the index of the collection that represents this instance. </summary>
        /// <value> Integer of item's index. </value>
        int Index { get; }
    }

    /// <summary> Represents an element in <see cref="IContainer{T}" /> with container dependency. </summary>
    /// <typeparam name="T"> The type of parent collection. </typeparam>
    public interface IContainable<out T> : IContainable
    {
        /// <summary> Gets the container holding this instance. </summary>
        /// <value> Container instance. </value>
        T Owner { get; }
    }
}