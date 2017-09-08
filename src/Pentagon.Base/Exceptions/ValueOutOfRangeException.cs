// -----------------------------------------------------------------------
//  <copyright file="ValueOutOfRangeException.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Exceptions
{
    using System;
    using System.Runtime.Serialization;
    using Helpers;

    /// <summary> The exception that is thrown when a string value is not valid. </summary>
    /// <typeparam name="T"> Type of the value. </typeparam>
    [Serializable]
    public class ValueOutOfRangeException<T> : ArgumentOutOfRangeException
        where T : IComparable<T>
    {
        /// <summary> Initializes a new instance of the <see cref="ValueOutOfRangeException{T}" /> class. </summary>
        /// <param name="paramName"> Name of the parameter. </param>
        /// <param name="actualValue"> The actual value. </param>
        /// <param name="range"> The range. </param>
        public ValueOutOfRangeException(string paramName, T actualValue, IRange<T> range) : base(paramName, actualValue, $"Given value ({actualValue}) is not in the range ({range}).") { }

        /// <summary> Initializes a new instance of the <see cref="ValueOutOfRangeException{T}" /> class. </summary>
        /// <param name="info"> The object that holds the serialized object data. </param>
        /// <param name="context"> An object that describes the source or destination of the serialized data. </param>
        protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}