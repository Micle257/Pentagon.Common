// -----------------------------------------------------------------------
//  <copyright file="DoubleExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using Pentagon.Extensions;
    using Xunit;

    public class DoubleExtensionsTests
    {
        [Theory]
        [InlineData(0.02d, 0.021, 0.01)]
        [InlineData(0.1d, 0.1, 0.01)]
        [InlineData(2.022d, 2.0218, 0.001)]
        public void EqualTo_ComparedValuesNotEqualButInTolerance_ReturnsTrue(double first, double second, double prec)
        {
            var result = first.EqualTo(second, prec);
            Assert.Equal(true, result);
        }

        [Fact]
        public void Mod_ModuleIsEqualToZero_Throws()
        {
            Assert.Throws<DivideByZeroException>(() => 2d.Mod(0));
        }

        [Theory]
        [InlineData(15, 5, 0)]
        [InlineData(32, 3, 2)]
        [InlineData(23, -3, -1)]
        [InlineData(-56, -23, -10)]
        [InlineData(-13, 7, 1)]
        public void Mod_ModuloIsNotEqualToZero_ReturnsCorrectValue(double value, int modulo, double expected)
        {
            var mod = value.Mod(modulo);

            Assert.Equal(expected, mod);
        }

        [Fact]
        public void RoundSignificantFigures_DigitCountIsLessThanOne_Throws()
        {
            Assert.Throws<ArgumentException>(() => 1d.RoundSignificantFigures(0));
        }

        [Theory]
        [InlineData(2557.4874, 2557.487, 7)]
        [InlineData(4.87, 5, 1)]
        [InlineData(2787557.4872124, 2787600, 5)]
        [InlineData(0.0000435, 0.000044, 2)]
        public void RoundSignificantFigures_ValueRound_ReturnsCorrectValue(double number, double expect, int figs)
        {
            var result = number.RoundSignificantFigures(figs);

            Assert.Equal(expect, result);
        }
    }
}