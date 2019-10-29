// -----------------------------------------------------------------------
//  <copyright file="IRange.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Ranges
{
    using System;

    /// <summary> Represents a range of values. </summary>
    /// <typeparam name="T"> The type of the value. </typeparam>
    public interface IRange<T>
        where T : IComparable<T>
    {
        /// <summary> Gets the down boundary. </summary>
        /// <value> The <see cref="T" />. </value>
        T Min { get; }

        /// <summary> Gets the up boundary. </summary>
        /// <value> The <see cref="T" />. </value>
        T Max { get; }

        /// <summary> Determines if given value is inside this range. </summary>
        /// <param name="value"> The value. </param>
        /// <returns> A <c> true </c> if value is in range; otherwise <c> false </c>. </returns>
        bool InRange(T value);
    }
}