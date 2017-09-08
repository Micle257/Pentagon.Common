// -----------------------------------------------------------------------
//  <copyright file="EnumerableExtensionsTests.cs" company="The Pentagon">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Root.Tests
{
    using System.Linq;
    using Extensions;
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