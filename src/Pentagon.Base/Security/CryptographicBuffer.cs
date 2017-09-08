// -----------------------------------------------------------------------
//  <copyright file="CryptographicBuffer.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Security
{
    using System;
    using JetBrains.Annotations;

    /// <summary> Represents a cryptographic buffer. </summary>
    [UsedImplicitly]
    public class CryptographicBuffer
    {
        /// <summary> Generates a random byte array of certain length. </summary>
        /// <param name="length"> The length. </param>
        /// <returns> A byte array with random data. </returns>
        public static byte[] GenerateRandom(int length)
        {
            var buffer = new byte[length];
            var r = new Random(0);
            r.NextBytes(buffer);
            return buffer;
        }
    }
}