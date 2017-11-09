// -----------------------------------------------------------------------
//  <copyright file="DoubleExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using Pentagon.Extensions;
    using Xunit;

    public class DoubleExtensionsTests
    {
        [Theory]
        [InlineData(0.02d, 0.021, 0.01)]
        [InlineData(0.1d, 0.1, 0.01)]
        [InlineData(2.022d, 2.0218, 0.001)]
        public void ShouldEqualToReturn(double first, double second, double prec)
        {
            var result = first.EqualTo(second, prec);
            Assert.Equal(true, result);
        }

        [Theory]
        [InlineData(2557.4874, 2557.487, 7)]
        [InlineData(4.87, 5, 1)]
        [InlineData(2787557.4872124, 2787600, 5)]
        public void ShouldSetSignificantDig(double number, double expect, int figs)
        {
            var result = number.SignificantFigures(figs);

            Assert.Equal(expect, result);
        }
    }
}