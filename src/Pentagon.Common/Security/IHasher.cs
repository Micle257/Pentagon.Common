namespace Pentagon.Security {
    using JetBrains.Annotations;

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
}