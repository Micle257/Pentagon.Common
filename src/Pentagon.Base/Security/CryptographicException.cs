// -----------------------------------------------------------------------
//  <copyright file="CryptographicException.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Security
{
    using System;
    using System.Runtime.Serialization;

    /// <summary> The exception that is thrown when a cryptographic process has error. </summary>
    /// <seealso cref="System.Exception" />
    [Serializable]
    public class CryptographicException : Exception
    {
        /// <summary> Initializes a new instance of the <see cref="CryptographicException" /> class. </summary>
        /// <param name="message"> The message that describes the error. </param>
        public CryptographicException(string message) : base(message) { }

        /// <summary> Initializes a new instance of the <see cref="CryptographicException" /> class. </summary>
        /// <param name="info"> The <see cref="T:System.Runtime.Serialization.SerializationInfo"> </see> that holds the serialized object data about the exception being thrown. </param>
        /// <param name="context"> The <see cref="T:System.Runtime.Serialization.StreamingContext"> </see> that contains contextual information about the source or destination. </param>
        protected CryptographicException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}