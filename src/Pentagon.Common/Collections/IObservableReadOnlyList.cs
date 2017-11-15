// -----------------------------------------------------------------------
//  <copyright file="IObservableReadOnlyList.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary> Represents an observable read-only collection that can be access by index. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public interface IObservableReadOnlyList<out T> : IReadOnlyList<T>, INotifyCollectionChanged { }
}