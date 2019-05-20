// -----------------------------------------------------------------------
//  <copyright file="QueueExtensions.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    /// <summary> Contains extension methods for <see cref="Queue{T}" />. </summary>
    public static class QueueExtensions
    {
        /// <summary> Removes and returns the object at the beginning; and adds an object to the end of the <see cref="Queue{T}" />. </summary>
        /// <typeparam name="T"> The type of an element. </typeparam>
        /// <param name="queue"> The queue. </param>
        /// <param name="value"> The value to enqueue. </param>
        /// <returns> The <see cref="T" /> that represents dequeued value from the queue. </returns>
        public static T Requeue<T>([NotNull] this Queue<T> queue, T value)
        {
            if (queue == null)
                throw new ArgumentNullException(nameof(queue));

            if (!queue.Any())
                throw new InvalidOperationException(message: "Empty queue cannot be dequeued.");

            var dequeuedValue = queue.Dequeue();
            queue.Enqueue(value);

            return dequeuedValue;
        }
    }
}