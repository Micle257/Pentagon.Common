// -----------------------------------------------------------------------
//  <copyright file="IValueDataType.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;

    /// <summary> Provides a way of specification <see cref="ValueType" /> behavior. </summary>
    /// <typeparam name="T"> Type of specified value. </typeparam>
    public interface IValueDataType<T> : IComparable, IComparable<T>, IEquatable<T>
    {
        /// <summary> Gets a value indicating whether this instance has value and is not default.
        ///     <para />
        ///     Equal to <c> this != default(T) </c>. </summary>
        /// <value> <c> true </c> if this instance has value; otherwise, <c> false </c>. </value>
        bool HasValue { get; }
    }
}