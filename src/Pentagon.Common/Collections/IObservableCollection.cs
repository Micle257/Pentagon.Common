// -----------------------------------------------------------------------
//  <copyright file="IObservableCollection.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Collections
{
    using System.Collections.Generic;
    using System.Collections.Specialized;

    /// <summary> Represents an observable collection. </summary>
    /// <typeparam name="T"> The type of an item. </typeparam>
    public interface IObservableCollection<T> : ICollection<T>, INotifyCollectionChanged { }
}