// -----------------------------------------------------------------------
//  <copyright file="EnumerableExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
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
        public void Append_ParameterIsNull_IteratesSameItems()
        {
            var collection = new[] {1, 2};

            var result = collection.Append(null);

            Assert.Equal(collection, result);
        }

        [Fact]
        public void Append_ParameterIsValid_AppendsItems()
        {
            var collection = new[] {1, 2};

            var added = new[] {6, 4};

            var result = collection.Append(added);

            Assert.Equal(new[] {1, 2, 6, 4}, result);
        }

        [Fact]
        public void Prepend_ParameterIsNull_IteratesSameItems()
        {
            var collection = new[] {1, 2};

            var result = collection.Prepend(null);

            Assert.Equal(collection, result);
        }

        [Fact]
        public void Prepend_ParameterIsValid_PrependsItems()
        {
            var collection = new[] {1, 2};

            var added = new[] {6, 4};

            var result = collection.Prepend(added);

            Assert.Equal(new[] {6, 4, 1, 2}, result);
        }

        [Fact]
        public void SymmetricExcept_ForTwoNonEmptyCollections_ReturnsCorrectValue()
        {
            var c1 = new[] {0, 1, 2, 3, 4, 5};
            var c2 = new[] {3, 4, 5, 6, 7};

            var except = c1.SymmetricExcept(c2);

            Assert.Equal(new[] {0, 1, 2, 6, 7}, except);
        }

        [Fact]
        public void GroupBySize_ForNonEmptyCollection_ReturnsCorrectValue()
        {
            var collection = new[] {1, 5, 9, 15, 71, 36, 1, 5, 97};

            var groupBySize = collection.GroupBySize(2);

            Assert.Equal(new[] {new[] {1, 5}, new[] {9, 15}, new[] {71, 36}, new[] {1, 5}, new[] {97}}, groupBySize);
        }

        [Fact]
        public void GroupBySize_ForNonEmptyCollection_ReturnsCorrectValue2()
        {
            var collection = new[] {1};

            var groupBySize = collection.GroupBySize(2);

            Assert.Equal(new[] {new[] {1}}, groupBySize);
        }

        [Fact]
        public void WhereNotNull_ForCollectionWithNullItems_ReturnsCorrectElementCount()
        {
            var collection = new[] { null, new object() };

            var filtered = collection.WhereNotNull();

            Assert.Equal(1, filtered.Count());
        }

        [Fact]
        public void OrderBySelf_ReturnsCorrectSequence()
        {
            var collection = new[] { 1, 6, 7, -6 };

            var filtered = collection.OrderBySelf();

            Assert.Equal(new [] {-6,1,6,7}, filtered);
        }

        [Fact]
        public void OrderBySelfDescending_ReturnsCorrectSequence()
        {
            var collection = new[] { 1, 6, 7, -6 };

            var filtered = collection.OrderBySelfDescending();

            Assert.Equal(new[] { 7,6,1,-6 }, filtered);
        }
    }
}