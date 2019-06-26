// -----------------------------------------------------------------------
//  <copyright file="TaskHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Threading
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary> Provides a helper and extension methods for <seealso cref="Task" />. </summary>
    public static class TaskHelper
    {
        /// <summary> Attempts invoking the function for desired limit count, if the function throws an exception. </summary>
        /// <typeparam name="T"> The type of the task result. </typeparam>
        /// <param name="function"> The function to invoke. </param>
        /// <param name="tryLimit"> The limit number of iterations. </param>
        /// <param name="iterationDelayInMilliseconds"> The iteration delay in milliseconds. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns> An asynchronous operation <see cref="Task{T}" />, which was passed in <paramref name="function" />. </returns>
        /// <exception cref="ArgumentNullException"> function or task </exception>
        /// <exception cref="ArgumentOutOfRangeException"> tryLimit - Try limit count must be greater or equal to one. </exception>
        /// <exception cref="NotSupportedException"> Internal error: this code should be unreachable. </exception>
        /// <remarks> Method is from Microsoft docs. </remarks>
        public static async Task<T> TryInvokeAsync<T>([NotNull] Func<Task<T>> function, int tryLimit, int iterationDelayInMilliseconds = 0, CancellationToken cancellationToken = default)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            if (tryLimit < 1)
                throw new ArgumentOutOfRangeException(nameof(tryLimit), tryLimit, message: "Try limit count must be greater or equal to one.");

            for (var i = 0; i < tryLimit; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    var task = function();

                    if (task == null)
                        throw new ArgumentNullException(nameof(task));

                    return await task.ConfigureAwait(false);
                }
                catch
                {
                    if (i == tryLimit - 1)
                        throw;
                }

                if (iterationDelayInMilliseconds > 0) // ensure there is delay, otherwise it could be infinity delay
                    await Task.Delay(iterationDelayInMilliseconds, cancellationToken);
            }

            throw new NotSupportedException(message: "Internal error: this code should be unreachable.");
        }

        /// <summary> Adds cancel wrapper around task. </summary>
        /// <typeparam name="T"> The type of the task's result. </typeparam>
        /// <param name="task"> The task. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns> A <see cref="Task{TResult}" /> that represent asynchronous operation. </returns>
        /// <exception cref="TaskCanceledException"> </exception>
        [ItemCanBeNull]
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>) s).TrySetResult(true), taskCompletionSource))
            {
                if (task != await Task.WhenAny(task, taskCompletionSource.Task))
                    throw new TaskCanceledException(task);
            }

            return task.Result;
        }

        /// <summary> Adds cancel wrapper around task. </summary>
        /// <param name="task"> The task. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns> A <see cref="Task" /> that represent asynchronous operation. </returns>
        /// <exception cref="TaskCanceledException"> </exception>
        public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>) s).TrySetResult(true), taskCompletionSource))
            {
                if (task != await Task.WhenAny(task, taskCompletionSource.Task))
                    throw new TaskCanceledException(task);
            }
        }

        /// <summary> Wraps the task around timeout logic. If timeout occurs, exception will be thrown. </summary>
        /// <typeparam name="TResult"> The type of the result. </typeparam>
        /// <param name="task"> The task. </param>
        /// <param name="timeout"> The timeout. </param>
        /// <param name="filePath"> The file path. </param>
        /// <param name="lineNumber"> The line number. </param>
        /// <returns> A <see cref="Task" /> that represents asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"> task </exception>
        /// <exception cref="TimeoutException"> </exception>
        public static async Task<TResult> TimeoutAfter<TResult>([NotNull] this Task<TResult> task,
                                                                TimeSpan timeout,
                                                                [CallerFilePath] string filePath = "",
                                                                [CallerLineNumber] int lineNumber = default)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.IsCompleted || Debugger.IsAttached)
                return await task.ConfigureAwait(false);

            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    return await task.ConfigureAwait(false); // Very important in order to propagate exceptions
                }

                throw new TimeoutException(CreateMessage(timeout, filePath, lineNumber));
            }
        }

        /// <summary> Wraps the task around timeout logic. If timeout occurs, exception will be thrown. </summary>
        /// <param name="task"> The task. </param>
        /// <param name="timeout"> The timeout. </param>
        /// <param name="filePath"> The file path. </param>
        /// <param name="lineNumber"> The line number. </param>
        /// <returns> A <see cref="Task" /> that represents asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"> task </exception>
        /// <exception cref="TimeoutException"> </exception>
        public static async Task TimeoutAfter([NotNull] this Task task,
                                              TimeSpan timeout,
                                              [CallerFilePath] string filePath = "",
                                              [CallerLineNumber] int lineNumber = default)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.IsCompleted || Debugger.IsAttached)
            {
                await task.ConfigureAwait(false);
                return;
            }

            using (var timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token));

                if (completedTask == task)
                {
                    timeoutCancellationTokenSource.Cancel();
                    await task.ConfigureAwait(false); // Very important in order to propagate exceptions
                    return;
                }

                throw new TimeoutException(CreateMessage(timeout, filePath, lineNumber));
            }
        }

        static string CreateMessage(TimeSpan timeout, string filePath, int lineNumber)
            => string.IsNullOrEmpty(filePath)
                       ? $"The operation timed out after reaching the limit of {timeout.TotalMilliseconds}ms."
                       : $"The operation at {filePath}:{lineNumber} timed out after reaching the limit of {timeout.TotalMilliseconds}ms.";
    
        private static readonly Action<Task> DefaultErrorContinuation =
                t =>
                {
                    try { t.Wait(); }
                    catch
                    {
                        // ignored
                    }
                };

        public static void RunAndForget([NotNull] Func<Task> action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            Task.Run(action).ConfigureAwait(false);
        }
    }
}