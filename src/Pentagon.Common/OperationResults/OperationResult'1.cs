// -----------------------------------------------------------------------
//  <copyright file="OperationResult'1.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.OperationResults
{
    using System;

    /// <summary> Represents a result from operation with encapsulated exception and strongly typed content. </summary>
    /// <typeparam name="TContent"> The type of the content. </typeparam>
    public class OperationResult<TContent> : OperationResult
    {
        public new static OperationResult<TContent> Success => new OperationResult<TContent>();

        /// <summary> Gets or sets the content of the result. </summary>
        /// <value> The <see cref="TContent" />. </value>
        public TContent Content { get; set; }

        #region Operators

        /// <summary> Performs an implicit conversion from <see cref="TContent" /> to <see cref="OperationResult{TContent}" />. </summary>
        /// <param name="content"> The content. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator OperationResult<TContent>(TContent content) => FromSuccess(content);

        /// <summary> Performs an implicit conversion from <see cref="Exception" /> to <see cref="OperationResult{TContent}" />. </summary>
        /// <param name="exception"> The exception. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator OperationResult<TContent>(Exception exception) => FromFailed(exception);

        /// <summary>
        /// Performs an implicit conversion from <see cref="VoidValue"/> to <see cref="OperationResult{TContent}"/>.
        /// </summary>
        /// <param name="voidValue">The void value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator OperationResult<TContent>(VoidValue voidValue) => Success;

        #endregion

        public static OperationResult<TContent> FromSuccess(TContent content) =>
                new OperationResult<TContent>
                {
                        Content = content
                };

        public new static OperationResult<TContent> FromFailed(Exception exception) =>
                new OperationResult<TContent>
                {
                        Exception = exception
                };
    }
}