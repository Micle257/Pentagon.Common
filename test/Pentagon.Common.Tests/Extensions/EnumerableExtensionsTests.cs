// -----------------------------------------------------------------------
//  <copyright file="EnumerableExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
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
            var collection = new[] { 1, 2 };

            var added = new[] {6, 4};

            var result = collection.Append(added);

            Assert.Equal(new [] {1,2,6,4 }, result);
        }

        [Fact]
        public void Prepend_ParameterIsNull_IteratesSameItems()
        {
            var collection = new[] { 1, 2 };

            var result = collection.Prepend(null);

            Assert.Equal(collection, result);
        }
        
        [Fact]
        public void Prepend_ParameterIsValid_PrependsItems()
        {
            var collection = new[] { 1, 2 };

            var added = new[] { 6, 4 };

            var result = collection.Prepend(added);

            Assert.Equal(new[] { 6, 4, 1, 2 }, result);
        }

        [Fact]
        public void SymmetricExcept_Scenario_ExpectedBehavior()
        {
            var c1 = new[] {0, 1, 2, 3, 4, 5};
            var c2 = new[] {3, 4, 5, 6, 7};

            var except = c1.SymmetricExcept(c2);

            Assert.Equal(new[] {0, 1, 2, 6, 7}, except);
        }

        [Fact]
        public void Requeue_ParameterQueueIsNull_Throws()
        {
            Queue<int> queue = null;

            Assert.Throws<ArgumentNullException>(() => queue.Requeue(5));
        }

        [Fact]
        public void Requeue_ParameterQueueIsEmpty_Throws()
        {
            var queue = new Queue<int>();

            Assert.Throws<ArgumentException>(() => queue.Requeue(5));
        }

        [Fact]
        public void Requeue_Invoke_ReturnsLastValue()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);

            var value = queue.Requeue(5);

            Assert.Equal(1, value);
        }

        [Fact]
        public void Requeue_Invoke_AddsValueToQueue()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);

            queue.Requeue(5);

            Assert.Equal(5, queue.Last());
        }

        [Fact]
        public void Requeue_Invoke_CountIsNotChanged()
        {
            var queue = new Queue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);

            queue.Requeue(5);

            Assert.Equal(2, queue.Count);
        }
    }
}