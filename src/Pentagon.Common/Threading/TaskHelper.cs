// -----------------------------------------------------------------------
//  <copyright file="TaskHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary> Provides a helper methods for <seealso cref="Task" />. </summary>
    public static class TaskHelper
    {
        /// <summary> Attempts invoking the function for desired limit count, if the function throws an exception. </summary>
        /// <remarks> Method is from Microsoft docs. </remarks>
        /// <typeparam name="T"> The type of the task result. </typeparam>
        /// <param name="function"> The function to invoke. </param>
        /// <param name="tryLimit"> The limit number of iterations. </param>
        /// <param name="iterationDelayInMilliseconds"> The iteration delay in milliseconds. </param>
        /// <returns> An asynchronous operation <see cref="Task{T}" />, which was passed in <paramref name="function" />. </returns>
        public static async Task<T> TryInvokeAsync<T>([NotNull] Func<Task<T>> function, int tryLimit, int iterationDelayInMilliseconds = 0)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            for (var i = 0; i < tryLimit; i++)
            {
                try
                {
                    var task = function();
                    if (task != null)
                        return await task.ConfigureAwait(false);
                }
                catch
                {
                    if (i == tryLimit - 1)
                        throw;
                }

                if (iterationDelayInMilliseconds > 0)
                    await Task.Delay(iterationDelayInMilliseconds).ConfigureAwait(false);
            }

            return default;
        }

        /// <summary> Wraps the task around timeout logic. If timeout occurs, exception will be thrown. </summary>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="task"> The task. </param>
        /// <param name="timeout"> The timeout. </param>
        /// <returns> A <see cref="Task" /> that represents asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"> task </exception>
        /// <exception cref="TimeoutException"> </exception>
        public static async Task<TResult> TimeoutAfter<TResult>([NotNull] this Task<TResult> task, TimeSpan timeout)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task.ConfigureAwait(false); // Very important in order to propagate exceptions
                }

                throw new TimeoutException();
            }
        }

        /// <summary> Wraps the task around timeout logic. If timeout occurs, exception will be thrown. </summary>
        /// <param name="task"> The task. </param>
        /// <param name="timeout"> The timeout. </param>
        /// <returns> A <see cref="Task" /> that represents asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"> task </exception>
        /// <exception cref="TimeoutException"> </exception>
        public static async Task TimeoutAfter([NotNull] this Task task, TimeSpan timeout)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    await task.ConfigureAwait(false); // Very important in order to propagate exceptions
                }

                throw new TimeoutException();
            }
        }
    }
}