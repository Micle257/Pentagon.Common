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
        public void FindOrderIndex_ConditionIsFalse_ReturnsFirstIndex()
        {
            var collection = new[] {0, 1, 2, 3, 4, 5};

            var order = collection.FindOrderIndex(n => false);

            Assert.Equal(0, order);
        }

        [Fact]
        public void FindOrderIndex_ConditionIsTrue_ReturnsLastIndex()
        {
            var collection = new[] {0, 1, 2, 3, 4, 5};

            var order = collection.FindOrderIndex(n => true);

            Assert.Equal(6, order);
        }

        [Fact]
        public void ShiftLeft_ShiftsCollectionToLeft()
        {
            var array = new[] {0, 1, 2, 3, 4, 5};

            var shifted = array.ShiftLeft().ToArray();

            Assert.Equal(new[] {1, 2, 3, 4, 5, 0}, shifted);
        }

        [Fact]
        public void ShiftRight_ShiftsCollectionToRight()
        {
            var array = new[] {0, 1, 2, 3, 4, 5};

            var shifted = array.ShiftRight().ToArray();

            Assert.Equal(new[] {5, 0, 1, 2, 3, 4}, shifted);
        }

        [Fact]
        public void SymmetricExcept_Scenario_ExpectedBehavior()
        {
            var c1 = new[] {0, 1, 2, 3, 4, 5};
            var c2 = new[] {3, 4, 5, 6, 7};

            var except = c1.SymmetricExcept(c2);

            Assert.Equal(new[] {0, 1, 2, 6, 7}, except);
        }
    }
}