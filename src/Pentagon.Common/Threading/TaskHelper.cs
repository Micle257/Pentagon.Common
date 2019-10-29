// -----------------------------------------------------------------------
//  <copyright file="TaskHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Threading
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary> Provides a helper and extension methods for <seealso cref="Task" />. </summary>
    [SuppressMessage("ReSharper", "AsyncConverter.AsyncMethodNamingHighlighting")]
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
        [PublicAPI]
        public static async Task<T> TryInvokeAsync<T>([JetBrains.Annotations.NotNull] Func<Task<T>> function, int tryLimit, int iterationDelayInMilliseconds = 0, CancellationToken cancellationToken = default)
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
        [PublicAPI]
        public static async Task<T> WithCancellation<T>([JetBrains.Annotations.NotNull] this Task<T> task, CancellationToken cancellationToken)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            var taskCompletionSource = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), taskCompletionSource))
            {
                if (task != await Task.WhenAny(task, taskCompletionSource.Task))
                    throw new TaskCanceledException(task);
            }

            return task.AwaitSynchronously();
        }

        /// <summary> Adds cancel wrapper around task. </summary>
        /// <param name="task"> The task. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns> A <see cref="Task" /> that represent asynchronous operation. </returns>
        /// <exception cref="TaskCanceledException"> </exception>
        [PublicAPI]
        public static async Task WithCancellation(this Task task, CancellationToken cancellationToken)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), taskCompletionSource))
            {
                if (task != await Task.WhenAny(task, taskCompletionSource.Task))
                    throw new TaskCanceledException(task);
            }
        }

        /// <summary>
        /// Wraps the task around timeout logic. If timeout occurs, exception will be thrown.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="continueOnCapturedContext">if set to <c>true</c> continue on captured context.</param>
        /// <returns>
        /// A <see cref="Task" /> that represents asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">task</exception>
        /// <exception cref="TimeoutException">timeout occured</exception>
        [PublicAPI]
        public static async Task<TResult> TimeoutAfter<TResult>([JetBrains.Annotations.NotNull] this Task<TResult> task,
                                                                TimeSpan timeout,
                                                                bool continueOnCapturedContext = true)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.IsCompleted)
                return await task.ConfigureAwait(continueOnCapturedContext);

            using var timeoutCancellationTokenSource = new CancellationTokenSource();

            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token)).ConfigureAwait(false);

            // if firstly completed operation is not input operation => operation reached timeout...
            if (completedTask != task)
                // throw
                throw new TimeoutException(CreateMessage(timeout));

            // cancel delay task
            timeoutCancellationTokenSource.Cancel();

            // return input operation to async context
            // await for propagating exceptions
            return await task.ConfigureAwait(continueOnCapturedContext);

        }

        /// <summary> Wraps the task around timeout logic. If timeout occurs, exception will be thrown. </summary>
        /// <param name="task">The task.</param>
        /// <param name="timeout">The timeout.</param>
        /// <param name="continueOnCapturedContext">if set to <c>true</c> continue on captured context.</param>
        /// <returns>
        /// A <see cref="Task" /> that represents asynchronous operation.
        /// </returns>
        /// <exception cref="ArgumentNullException">task</exception>
        /// <exception cref="TimeoutException">timeout occured</exception>
        [PublicAPI]
        public static async Task TimeoutAfter([JetBrains.Annotations.NotNull] this Task task,
                                              TimeSpan timeout,
                                              bool continueOnCapturedContext = true)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (task.IsCompleted)
            {
                await task.ConfigureAwait(continueOnCapturedContext);
                return;
            }

            using var timeoutCancellationTokenSource = new CancellationTokenSource();

            var completedTask = await Task.WhenAny(task, Task.Delay(timeout, timeoutCancellationTokenSource.Token)).ConfigureAwait(false);

            // if firstly completed operation is not input operation => operation reached timeout...
            if (completedTask != task)
                    // throw
                throw new TimeoutException(CreateMessage(timeout));

            // cancel delay task
            timeoutCancellationTokenSource.Cancel();

            // await for propagating exceptions
            await task.ConfigureAwait(continueOnCapturedContext);
        }

        /// <summary> Safely execute the Task without waiting for it to complete before moving to the next line of code. </summary>
        /// <param name="callback"> Callback function. </param>
        /// <param name="continueOnCapturedContext"> If set to <c> true </c> continue on captured context; this will ensure that the Synchronization Context returns to the calling thread. If set to <c> false </c> continue on a different context; this will allow the Synchronization Context to continue on a different thread </param>
        /// <param name="onException"> If an exception is thrown in the Task, <c> onException </c> will execute. If onException is null, the exception will be re-thrown </param>
        [PublicAPI]
        public static void RunAndForget([JetBrains.Annotations.NotNull] Func<Task> callback,
                                        bool continueOnCapturedContext = true,
                                        Action<Exception> onException = null)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            var task = Task.Run(callback);

            if (onException != null)
            {
                task.ContinueWith(a => onException(a.Exception.GetBaseException()), TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);
            }
            else
                task.ContinueWith(DefaultErrorContinuation, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);

            if (!continueOnCapturedContext)
                task.ConfigureAwait(false);

            static void DefaultErrorContinuation(Task task)
            {
                try
                {
                    task.AwaitSynchronously();
                }
                catch
                {
                    // ignore
                }
            };
        }

        /// <summary> Safely execute the Task without waiting for it to complete before moving to the next line of code. </summary>
        /// <param name="task"> Task. </param>
        /// <param name="continueOnCapturedContext"> If set to <c> true </c> continue on captured context; this will ensure that the Synchronization Context returns to the calling thread. If set to <c> false </c> continue on a different context; this will allow the Synchronization Context to continue on a different thread </param>
        /// <param name="onException"> If an exception is thrown in the Task, <c> onException </c> will execute. If onException is null, the exception will be re-thrown </param>
        [SuppressMessage(category: "ReSharper", checkId: "AvoidAsyncVoid")]
        [PublicAPI]
        public static async void RunAndForget([JetBrains.Annotations.NotNull] this Task task,
                                              bool continueOnCapturedContext = true,
                                              Action<Exception> onException = null)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }

        /// <summary> Helper for awaiting task in synchronous context. Calls GetAwaiter and GetResult -> will throw exception from task. </summary>
        /// <typeparam name="TValue"> The type of the value. </typeparam>
        /// <param name="task"> The task. </param>
        /// <returns> The result of asynchronous operation. </returns>
        /// <exception cref="ArgumentNullException"> task </exception>
        [PublicAPI]
        public static TValue AwaitSynchronously<TValue>([JetBrains.Annotations.NotNull] this Task<TValue> task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            return task.GetAwaiter().GetResult();
        }

        /// <summary> Helper for awaiting task in synchronous context. Calls GetAwaiter and GetResult -> will throw exception from task. </summary>
        /// <param name="task"> The task. </param>
        /// <exception cref="ArgumentNullException"> task </exception>
        [PublicAPI]
        public static void AwaitSynchronously([JetBrains.Annotations.NotNull] this Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            task.GetAwaiter().GetResult();
        }

        static string CreateMessage(TimeSpan timeout)
            => $"The operation timed out after reaching the limit of {timeout.TotalMilliseconds}ms.";
    }
}