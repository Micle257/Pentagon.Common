// -----------------------------------------------------------------------
//  <copyright file="ISelectable.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using Collections;
    
    /// <summary> Provides an ability of the object to dynamically select an element from the container. </summary>
    /// <typeparam name="T"> The type of the element. </typeparam>
    public interface ISelectable<T> : IContainer<T>
    {
        /// <summary> Occurs when the element is selected. </summary>
        event SelectedEventHandler<T> Selected;

        /// <summary> Gets the currently selected element. </summary>
        T Current { get; }

        /// <summary> Selects the specified element. </summary>
        /// <param name="obj"> The object to select. </param>
        void Select(T obj);

        /// <summary> Selects the next element (or first if current is the last element) in collection. </summary>
        void SelectNext();

        /// <summary> Selects the previous element (or last if current is the first element) in collection. </summary>
        void SelectPrevious();
    }
}