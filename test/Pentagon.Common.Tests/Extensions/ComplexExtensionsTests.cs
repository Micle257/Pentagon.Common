// -----------------------------------------------------------------------
//  <copyright file="ComplexExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System.Numerics;
    using Pentagon.Extensions;
    using Xunit;

    public class ComplexExtensionsTests
    {
        [Fact]
        public void EqualTo_ComparedValuesNotEqualButInTolerance_ReturnsTrue()
        {
            var c1 = new Complex(0.02, 2);
            var c2 = new Complex(0.021, 2.0001);

            var result = c1.EqualTo(c2, .01);
            Assert.Equal(true, result);
        }
    }
}