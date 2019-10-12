// -----------------------------------------------------------------------
//  <copyright file="IHasRange.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;
    using Helpers;

    /// <summary> Represents an ability of type to have range over some value. </summary>
    public interface IHasRange<T>
        where T : IEquatable<T>, IComparable<T>
    {
        /// <summary> Gets the range of the value. </summary>
        /// <value> The <see cref="Range{T}" />. </value>
        IRange<T> Range { get; }
    }
}