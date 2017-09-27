// -----------------------------------------------------------------------
//  <copyright file="IValuable.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    /// <summary> Represents an ability of type to be access by major value. </summary>
    public interface IValuable<out T>
    {
        /// <summary> Gets the value. </summary>
        T Value { get; }
    }
}