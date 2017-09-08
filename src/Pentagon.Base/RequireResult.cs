// -----------------------------------------------------------------------
//  <copyright file="RequireResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon
{
    using System;

    /// <summary> Represents a require result. </summary>
    /// <typeparam name="TException"> The type of the exception. </typeparam>
    public class RequireResult<TException> : IRequireResult
        where TException : Exception
    {
        /// <summary> Gets the exception. </summary>
        /// <value> The <see cref="TException" />. </value>
        public TException Exception { get; internal set; }

        /// <inheritdoc />
        public bool IsValid => Exception == null;
    }
}