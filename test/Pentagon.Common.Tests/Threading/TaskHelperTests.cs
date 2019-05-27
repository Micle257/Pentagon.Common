﻿// -----------------------------------------------------------------------
//  <copyright file="TaskHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Threading
{
    using System;
    using System.Threading.Tasks;
    using Pentagon.Threading;
    using Xunit;

    public class TaskHelperTests
    {
        [Fact]
        public void TryInvokeAsync_RetryCountExceeded_Throws()
        {
            Func<Task<bool>> func = () => Task.Run((Func<bool>) (() => throw new ArgumentException()));

            Assert.Throws<AggregateException>(() => TaskHelper.TryInvokeAsync(func, 5).Wait());
        }

        [Fact]
        public void TryInvokeAsync_RetryCountIsGreatenThanFunctionFailureCount_InvokesFunction()
        {
            Func<Task<bool>> func = () => Task.Run(() => true);

            var result = TaskHelper.TryInvokeAsync(func, 2).Result;

            Assert.True(result);
        }

        [Fact]
        public Task AfterTimeout_TaskTimeout_Throws()
        {
            var longRunningTask = Task.Delay(2000);

            return Assert.ThrowsAsync<TimeoutException>(() => longRunningTask.TimeoutAfter(TimeSpan.FromSeconds(1)));
        }

        [Fact]
        public Task AfterTimeoutGeneric_TaskTimeout_Throws()
        {
            var longRunningTask = Task.Run(async () =>
                                           {
                                               await Task.Delay(2000);
                                               return 3;
                                           });

            return Assert.ThrowsAsync<TimeoutException>(() => longRunningTask.TimeoutAfter(TimeSpan.FromSeconds(1)));
        }
    }
}