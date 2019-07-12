// -----------------------------------------------------------------------
//  <copyright file="RetryHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.PerformanceTests.Helpers
{
    using System;
    using BenchmarkDotNet.Attributes;
    using Pentagon.Helpers;
    using Xunit;

    public class RetryHelperBenchmarks
    {
        [Params(1,2)]
        public int Times;

        [Params(100, 200)]
        public int Timeout;

        [Benchmark]
        public void RetryOnException()
        {
            Assert.Throws<ArgumentException>(() => RetryHelper.RetryOnException(Callback, Times, TimeSpan.FromMilliseconds(Timeout)));

            void Callback()
            {
                throw new ArgumentException("Something is wrong.");
            }
        }
    }
}