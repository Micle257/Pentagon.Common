// -----------------------------------------------------------------------
//  <copyright file="Harness.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Common.PerformanceTests {
    using BenchmarkDotNet.Running;
    using Helpers;
    using Xunit;

    /// <summary>
    /// Wrappers that make it easy to run benchmark suites through the <c>dotnet test</c> runner.
    /// </summary>
    /// <example>
    /// <code>dotnet test -c Release -f netcoreapp2.2 --filter "FullyQualifiedName=Serilog.PerformanceTests.Harness.Allocations"</code>
    /// </example>
    public class EntryPoint
    {
        [Fact]
        public void RetryHelper()
        {
            BenchmarkRunner.Run<RetryHelperBenchmarks>();
        }
    }
}