// -----------------------------------------------------------------------
//  <copyright file="EnumerableExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System.Linq;
    using Pentagon.Extensions;
    using Xunit;

    public class EnumerableExtensionsTests
    {
        [Fact]
        public void ShouldShiftLeft()
        {
            var array = new[] {0, 1, 2, 3, 4, 5};
            var shifted = array.ShiftLeft().ToArray();
            Assert.True(shifted.SequenceEqual(new[] {1, 2, 3, 4, 5, 0}));
        }

        [Fact]
        public void ShouldShiftRight()
        {
            var array = new[] {0, 1, 2, 3, 4, 5};
            var shifted = array.ShiftRight().ToArray();
            Assert.True(shifted.SequenceEqual(new[] {5, 0, 1, 2, 3, 4}));
        }
    }
}