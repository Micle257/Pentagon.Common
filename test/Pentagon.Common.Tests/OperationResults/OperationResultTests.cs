namespace Pentagon.Common.Tests.OperationResults {
    using System;
    using Pentagon.OperationResults;
    using Xunit;

    public class OperationResultTests
    {
        [Fact]
        public void ImplicitSuccess_Works()
        {
            // ARRANGE
            OperationResult<string> Do(bool c)
            {
                if (c)
                    return new Exception("sa");

                return VoidValue.Value;
            }

            OperationResult Do2(bool c)
            {
                if (c)
                    return new Exception("sa");

                return VoidValue.Value;
            }

            // ACT

            // ASSERT
        }
    }
}