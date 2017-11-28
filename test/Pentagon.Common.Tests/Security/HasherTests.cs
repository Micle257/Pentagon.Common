// -----------------------------------------------------------------------
//  <copyright file="HasherTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Common.Tests.Security
{
    using Exceptions;
    using Pentagon.Security;
    using Xunit;

    public class HasherTests
    {
        IHasher _hasher;

        public HasherTests()
        {
            _hasher = new Hasher();
        }

        [Fact]
        public void VerifyHashedPassword_PasswordIsCorrect_ReturnsTrue()
        {
            var hash = _hasher.HashPassword("pass");
            var result = _hasher.VerifyHashedPassword(hash, "pass");

            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashPassword_PasswordHasInvalidFormat_Throws(string value)
        {
            Assert.Throws<StringArgumentException>(() => _hasher.HashPassword(value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashPassword_2_PasswordHasInvalidFormat_Throws(string value)
        {
            Assert.Throws<StringArgumentException>(() => _hasher.HashPassword(value, "valid"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HashPassword_2_SaltHasInvalidFormat_Throws(string value)
        {
            Assert.Throws<StringArgumentException>(() => _hasher.HashPassword("valid", value));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void VerifyHashedPassword_HashedPasswordHasInvalidFormat_Throws(string value)
        {
            Assert.Throws<StringArgumentException>(() => _hasher.VerifyHashedPassword(value, "valid"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void VerifyHashedPassword_ProvidedPasswordHasInvalidFormat_Throws(string value)
        {
            Assert.Throws<StringArgumentException>(() => _hasher.VerifyHashedPassword( "valid", value));
        }
    }
}