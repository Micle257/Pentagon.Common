// -----------------------------------------------------------------------
//  <copyright file="RetryHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Helpers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary> Provides helper logic for re-trying operations. </summary>
    public static class RetryHelper
    {
        /// <summary> Retries the given operation with specified delay and number of retry attempts. Can configure what exception are "retry-able". Also supports dynamic delay change. </summary>
        /// <param name="operation"> The operation. </param>
        /// <param name="times"> The times. </param>
        /// <param name="delay"> The delay. </param>
        /// <param name="exceptionPredicate"> The exception predicate. </param>
        /// <param name="forceThrowCallback"> The force throw callback. </param>
        /// <param name="incrementDelay"> The increment delay. </param>
        /// <param name="maxDelay"> The maximum delay. </param>
        /// <exception cref="System.ArgumentNullException"> operation </exception>
        public static void RetryOnException(Action operation,
                                            int times,
                                            TimeSpan delay,
                                            Func<Exception, RetryContext, bool> exceptionPredicate = null,
                                            Func<Exception, bool> forceThrowCallback = null,
                                            TimeSpan? incrementDelay = null,
                                            TimeSpan? maxDelay = null)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            exceptionPredicate = exceptionPredicate ?? ((_, __) => true);

            var attempts = 0;

            do
            {
                try
                {
                    attempts++;
                    operation();
                    break;
                }
                catch (Exception ex) when (exceptionPredicate(ex, new RetryContext {AttemptNumber = attempts, Exception = ex}))
                {
                    if (attempts >= times || (forceThrowCallback?.Invoke(ex) ?? false))
                        throw;

                    var dynamicDelay = delay.Add(incrementDelay.HasValue ? TimeSpan.FromTicks((attempts - 1) * incrementDelay.Value.Ticks) : TimeSpan.Zero);

                    dynamicDelay = maxDelay.HasValue ? dynamicDelay > maxDelay.Value ? maxDelay.Value : dynamicDelay : dynamicDelay;

                    Thread.Sleep(dynamicDelay);
                }
            } while (true);
        }

        /// <summary> Retries the given operation with specified delay and number of retry attempts. Can configure what exception are "retry-able". Also supports dynamic delay change. </summary>
        /// <param name="operation"> The operation. </param>
        /// <param name="times"> The times. </param>
        /// <param name="delay"> The delay. </param>
        /// <param name="exceptionPredicate"> The exception predicate. </param>
        /// <param name="forceThrowCallback"> The force throw callback. </param>
        /// <param name="incrementDelay"> The increment delay. </param>
        /// <param name="maxDelay"> The maximum delay. </param>
        /// <exception cref="System.ArgumentNullException"> operation </exception>
        public static async Task RetryOnExceptionAsync([NotNull] Func<Task> operation,
                                                       int times,
                                                       TimeSpan delay,
                                                       Func<Exception, RetryContext, bool> exceptionPredicate = null,
                                                       Func<Exception, bool> forceThrowCallback = null,
                                                       TimeSpan? incrementDelay = null,
                                                       TimeSpan? maxDelay = null)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            exceptionPredicate = exceptionPredicate ?? ((_, __) => true);

            var attempts = 0;

            do
            {
                try
                {
                    attempts++;
                    await operation();
                    break;
                }
                catch (Exception ex) when (exceptionPredicate(ex, new RetryContext {AttemptNumber = attempts, Exception = ex}))
                {
                    if (attempts >= times || (forceThrowCallback?.Invoke(ex) ?? false))
                        throw;

                    var dynamicDelay = delay.Add(incrementDelay.HasValue ? TimeSpan.FromTicks((attempts - 1) * incrementDelay.Value.Ticks) : TimeSpan.Zero);

                    dynamicDelay = maxDelay.HasValue ? dynamicDelay > maxDelay.Value ? maxDelay.Value : dynamicDelay : dynamicDelay;

                    await Task.Delay(dynamicDelay).ConfigureAwait(false);
                }
            } while (true);
        }

        /// <summary> Retries the given operation with specified delay and number of retry attempts. Can configure what exception are "retry-able". Also supports dynamic delay change. </summary>
        /// <param name="operation"> The operation. </param>
        /// <param name="times"> The times. </param>
        /// <param name="delay"> The delay. </param>
        /// <param name="exceptionPredicate"> The exception predicate. </param>
        /// <param name="forceThrowCallback"> The force throw callback. </param>
        /// <param name="incrementDelay"> The increment delay. </param>
        /// <param name="maxDelay"> The maximum delay. </param>
        /// <exception cref="System.ArgumentNullException"> operation </exception>
        public static async Task<T> RetryOnExceptionAsync<T>([NotNull] Func<Task<T>> operation,
                                                             int times,
                                                             TimeSpan delay,
                                                             Func<Exception, RetryContext, bool> exceptionPredicate = null,
                                                             Func<Exception, bool> forceThrowCallback = null,
                                                             TimeSpan? incrementDelay = null,
                                                             TimeSpan? maxDelay = null)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            exceptionPredicate = exceptionPredicate ?? ((_, __) => true);

            var attempts = 0;
            T result;

            do
            {
                try
                {
                    attempts++;
                    result = await operation();
                    break;
                }
                catch (Exception ex) when (exceptionPredicate(ex, new RetryContext {AttemptNumber = attempts, Exception = ex}))
                {
                    if (attempts >= times || (forceThrowCallback?.Invoke(ex) ?? false))
                        throw;

                    var dynamicDelay = delay.Add(incrementDelay.HasValue ? TimeSpan.FromTicks((attempts - 1) * incrementDelay.Value.Ticks) : TimeSpan.Zero);

                    dynamicDelay = maxDelay.HasValue ? dynamicDelay > maxDelay.Value ? maxDelay.Value : dynamicDelay : dynamicDelay;

                    await Task.Delay(dynamicDelay).ConfigureAwait(false);
                }
            } while (true);

            return result;
        }

        public class RetryContext
        {
            public int AttemptNumber { get; set; }

            public Exception Exception { get; set; }
        }
    }
}