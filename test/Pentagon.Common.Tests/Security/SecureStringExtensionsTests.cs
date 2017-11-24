// -----------------------------------------------------------------------
//  <copyright file="SecureStringExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Common.Tests.Security
{
    using System;
    using System.Security;
    using Pentagon.Security;
    using Xunit;

    public class SecureStringExtensionsTests
    {
        [Fact]
        public void ConvertToString_ParameterIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => SecureStringExtensions.ConvertToString(null));
        }

        [Fact]
        public void ConvertToString_Scenario_ReturnsCorectString()
        {
            var value = "test data";

            var secure = new SecureString();

            foreach (var c in value)
            {
                secure.AppendChar(c);
            }

            var convertedValue = secure.ConvertToString();

            Assert.Equal(value, convertedValue);
        }
    }
}