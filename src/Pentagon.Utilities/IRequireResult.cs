// -----------------------------------------------------------------------
//  <copyright file="IRequireResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    /// <summary> Provides an interface for <see cref="RequireResult{TException}" />. </summary>
    public interface IRequireResult
    {
        /// <summary> Returns true if the require operation is valid. </summary>
        /// <value> <c> true </c> if require is valid; otherwise, <c> false </c>. </value>
        bool IsValid { get; }
    }
}