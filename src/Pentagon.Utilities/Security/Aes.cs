﻿// -----------------------------------------------------------------------
//  <copyright file="Aes.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Security
{
    using System;
    using System.Linq;
    using System.Text;
    using Exceptions;
    using JetBrains.Annotations;

    /// <summary> Provides an Advanced Encryption Standard for the encryption of digital data established by the NIST. </summary>
    public class Aes
    {
        /// <summary> The key size. </summary>
        const int KeySize = 256;

        /// <summary> The block size. </summary>
        const int BlockSize = 128;

        /// <summary> The inner provider. </summary>
        [NotNull]
        readonly System.Security.Cryptography.Aes _provider;

        /// <summary> The key. </summary>
        readonly byte[] _key;

        /// <summary> The IV vector. </summary>
        byte[] _iv;

        /// <summary> Initializes a new instance of the <see cref="Aes" /> class. </summary>
        /// <param name="password"> The password. </param>
        /// <param name="createIv"> Indicates usage of IV vector. </param>
        /// <exception cref="ArgumentException"> Password value must have characters. </exception>
        public Aes([NotNull] string password, bool createIv = true)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));
            CreateIv = createIv;
            _provider = System.Security.Cryptography.Aes.Create() ?? throw new ArgumentNullException();
            _provider.BlockSize = BlockSize;
            _provider.KeySize = KeySize;

            _provider.GenerateKey();
            _key = _provider.Key;
        }

        /// <summary> Gets a value indicating whether this AES instance creates IV vector on encryption. </summary>
        /// <value> <c> true </c> if create iv; otherwise, <c> false </c>. </value>
        public bool CreateIv { get; }

        /// <summary> Decrypts the specified cipher text. </summary>
        /// <param name="cipherText"> The cipher text. </param>
        /// <param name="plainText"> The plain text as output. </param>
        /// <returns> <c> true </c> if decryption was successful; otherwise, <c> false </c>. </returns>
        public bool Decrypt(string cipherText, out string plainText)
        {
            var cipherTextBuffer = Convert.FromBase64String(cipherText);
            _iv = CreateIv ? cipherTextBuffer.Take(_provider.BlockSize).ToArray() : null;
            var decryptor = _provider.CreateDecryptor(_key, _iv);
            if (decryptor == null)
                throw new CryptographicException(message: "Decrypter initialization failed.");
            try
            {
                var plainTextBuffer = decryptor.TransformFinalBlock(cipherTextBuffer, _iv?.Length ?? 0, cipherTextBuffer.Length - (_iv?.Length ?? 0));
                plainText = Encoding.Unicode.GetString(plainTextBuffer, 0, plainTextBuffer.Length);
                if (plainText == string.Empty)
                    throw new FormatException();
                return true;
            }
            catch
            {
                plainText = null;
                return false;
            }
        }

        /// <summary> Encrypts the specified cipher text. </summary>
        /// <param name="plainText"> The plain text to encrypt. </param>
        /// <returns> A <see cref="string" /> representing the ciphered data. </returns>
        public string Encrypt([NotNull] string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));
            var plainTextBuffer = Encoding.Unicode.GetBytes(plainText);
            _provider.GenerateIV();
            _iv = CreateIv ? _provider.IV : null;
            var encryptor = _provider.CreateEncryptor(_key, _iv);
            if (encryptor == null)
                throw new CryptographicException(message: "Encrypter initialization failed.");
            var cipherTextBuffer = encryptor.TransformFinalBlock(plainTextBuffer, 0, plainTextBuffer.Length);
            var secureBuffer = cipherTextBuffer;
            if (_iv != null)
                secureBuffer = _iv.Concat(cipherTextBuffer).ToArray();
            return Convert.ToBase64String(secureBuffer, 0, secureBuffer.Length);
        }
    }
}