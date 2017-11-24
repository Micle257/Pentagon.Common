// -----------------------------------------------------------------------
//  <copyright file="RandomHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Common.Tests.Security
{
    using Pentagon.Security;
    using Xunit;

    public class RandomHelperTests
    {
        [Fact]
        public void GenerateRandom_LengthIsSameForTwoExecutions_ReturnedValuesAreNotEqual()
        {
            var length = 3;

            var v1 = RandomHelper.GenerateRandom(length);
            var v2 = RandomHelper.GenerateRandom(length);

            Assert.NotEqual(v1, v2);
        }
    }
}