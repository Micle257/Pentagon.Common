// -----------------------------------------------------------------------
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