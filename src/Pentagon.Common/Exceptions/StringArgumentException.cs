// -----------------------------------------------------------------------
//  <copyright file="StringArgumentException.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    /// <summary> The exception that is thrown when a string value is not valid. </summary>
    [Serializable]
    public class StringArgumentException : ArgumentException
    {
        /// <summary> Initializes a new instance of the <see cref="StringArgumentException" /> class. </summary>
        public StringArgumentException() : base(GetMessage()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringArgumentException" /> class.
        /// </summary>
        /// <param name="error">The message specifing the error.</param>
        /// <param name="paramName">Name of the parameter.</param>
        public StringArgumentException(string error, string paramName) : base(GetMessage(error), paramName) { }

        /// <summary> Initializes a new instance of the <see cref="StringArgumentException" /> class. </summary>
        /// <param name="error"> The message specifing the error. </param>
        /// <param name="innerException"> The inner exception. </param>
        public StringArgumentException(string error, Exception innerException) : base(GetMessage(error), innerException) { }

        /// <summary> Initializes a new instance of the <see cref="StringArgumentException" /> class. </summary>
        /// <param name="info"> The object that holds the serialized object data. </param>
        /// <param name="context"> The contextual information about the source or destination. </param>
        protected StringArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        /// <summary> Converts the inner message to exception message. </summary>
        /// <param name="inner"> The inner. </param>
        /// <returns> The <see cref="string" /> value of message. </returns>
        static string GetMessage(string inner = null) => $"String value is not valid{(inner == null ? "" : $" string should be {inner})")}.";
    }
}