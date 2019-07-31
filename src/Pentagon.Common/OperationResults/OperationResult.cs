// -----------------------------------------------------------------------
//  <copyright file="OperationResult.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.OperationResults
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    /// <summary> Represents a result from operation with encapsulated exception. </summary>
    public class OperationResult
    {
        /// <summary> Gets the succeeded operation result. </summary>
        /// <value> The <see cref="OperationResult" />. </value>
        public static OperationResult Success => new OperationResult();

        /// <summary> Gets a value indicating whether operation that this object represents succeeded. </summary>
        /// <value> <c> true </c> if the operation is successful; otherwise, <c> false </c>. </value>
        public bool IsSuccessful => Exception == null;

        /// <summary> Gets or sets the exception. </summary>
        /// <value> The exception. </value>
        public Exception Exception { get; set; }

        #region Operators

        /// <summary> Performs an implicit conversion from <see cref="Exception" /> to <see cref="OperationResult" />. </summary>
        /// <param name="exception"> The exception. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator OperationResult(Exception exception) => FromFailed(exception);

        /// <summary> Performs an implicit conversion from <see cref="VoidValue" /> to <see cref="OperationResult" />. </summary>
        /// <param name="voidValue"> The void value. </param>
        /// <returns> The result of the conversion. </returns>
        public static implicit operator OperationResult(VoidValue voidValue) => Success;

        #endregion

        public static OperationResult FromFailed(Exception exception) =>
                new OperationResult
                {
                        Exception = exception
                };

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder(value: "Result: ");

            sb.Append(IsSuccessful ? "succeeded" : "failed");

            if (Exception != null)
                sb.Append($": {Exception.Message}");

            return sb.ToString();
        }

        /// <summary> Throws if operation failed. </summary>
        [SuppressMessage(category: "ReSharper", checkId: "UnthrowableException")]
        public void ThrowIfFailed()
        {
            if (Exception != null && !IsSuccessful)
                throw Exception;
        }
    }
}