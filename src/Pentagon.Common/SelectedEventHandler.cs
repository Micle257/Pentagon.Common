// -----------------------------------------------------------------------
//  <copyright file="SelectedEventHandler.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using EventArguments;

    /// <summary> Represents an event handler method for managing selection of element event. </summary>
    /// <typeparam name="T"> Type of the element. </typeparam>
    /// <param name="source"> The instance of the event provider. </param>
    /// <param name="args"> The <see cref="SelectedEventArgs{T}" /> instance containing the event data. </param>
    public delegate void SelectedEventHandler<T>(object source, SelectedEventArgs<T> args);
}