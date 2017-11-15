// -----------------------------------------------------------------------
//  <copyright file="Hasher.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Security
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using JetBrains.Annotations;
    using Registration;

    /// <summary> Provides a hasher algorithm. </summary>
    public interface IHasher 
    {
        /// <summary> Gets the random salt used to complicate password. </summary>
        /// <value> The salt. </value>
        string Salt { get; }

        /// <summary> Gets the hashed password with salt. </summary>
        /// <value> The hash. </value>
        string Hash { get; }

        /// <summary> Generates the hash and random salt for given password. </summary>
        /// <param name="newPass"> The password. </param>
        void GenerateHashSalt([NotNull] string newPass);

        /// <summary> Generates the hash with given password and salt. </summary>
        /// <param name="newPass"> The new pass. </param>
        /// <param name="salt"> The salt. </param>
        void GenerateHash([NotNull] string newPass, [NotNull] string salt);

        /// <summary> Compares the this hasher with another hash value. </summary>
        /// <param name="dbHash"> The other hash to compare. </param>
        /// <returns> <c> true </c> if hashes are equal; otherwise <c> false </c>. </returns>
        bool CompareHash(string dbHash) ;
    }

    /// <summary> Provides a hasher algorithm (SHA512). </summary>
    [Register]
    public sealed class Hasher : IHasher
    {
        /// <inheritdoc />
        public string Salt { get; private set; }

        /// <inheritdoc />
        public string Hash { get; private set; }

        /// <summary> Generates the random salt. </summary>
        /// <param name="size"> The byte size of salt. </param>
        /// <returns> Byte array of salt. </returns>
        public static byte[] GenerateSalt(int size) => RandomHelper.GenerateRandom(size);

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException"> when <paramref name="newPass" /> is <see langword="null" />. </exception>
        public void GenerateHashSalt(string newPass)
        {
            if (newPass == null)
                throw new ArgumentNullException(nameof(newPass));
            Salt = Convert.ToBase64String(GenerateSalt(32));
            Hash = Convert.ToBase64String(GenerateSaltedHash(newPass, Convert.FromBase64String(Salt)));
        }

        /// <inheritdoc />
        /// <exception cref="ArgumentNullException">when <paramref name="newPass" /> -or- <paramref name="salt" /> are <see langword="null" />. </exception>
        public void GenerateHash( string newPass, string salt)
        {
            if (newPass == null)
                throw new ArgumentNullException(nameof(newPass));
            Salt = salt ?? throw new ArgumentNullException(nameof(salt));
            Hash = Convert.ToBase64String(GenerateSaltedHash(newPass, Convert.FromBase64String(salt)));
        }

        /// <inheritdoc />
        public bool CompareHash(string dbHash) => string.CompareOrdinal(Hash, dbHash) == 0;

        /// <summary> Generates the hash with give password and salt. </summary>
        /// <param name="password"> The password. </param>
        /// <param name="salt"> The salt. </param>
        /// <returns> </returns>
        byte[] GenerateSaltedHash(string password, byte[] salt)
        {
            var text = Encoding.UTF8?.GetBytes(password);
            var managed = SHA512.Create();
            var textWithSalt = text.Concat(salt).ToArray();
            return managed.ComputeHash(textWithSalt);
        }
    }
}