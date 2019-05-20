namespace Pentagon.Common.Tests.Extensions {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Pentagon.Extensions;
    using Xunit;

    public class QueueExtensionsTests
    {
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

            Assert.Throws<InvalidOperationException>(() => queue.Requeue(5));
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