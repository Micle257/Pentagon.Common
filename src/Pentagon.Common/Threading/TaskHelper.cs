// -----------------------------------------------------------------------
//  <copyright file="TaskHelper.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Threading
{
    using System;
    using System.Threading.Tasks;
    using JetBrains.Annotations;

    /// <summary> Provides a helper methods for <seealso cref="Task" />. </summary>
    public class TaskHelper
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
                // ReSharper disable once CatchAllClause
                catch
                {
                    if (i == tryLimit - 1)
                        throw;
                }

                if (iterationDelayInMilliseconds > 0)
                        // ReSharper disable once PossibleNullReferenceException
                    await Task.Delay(iterationDelayInMilliseconds).ConfigureAwait(false);
            }

            return default;
        }
    }
}