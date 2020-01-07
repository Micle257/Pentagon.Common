// -----------------------------------------------------------------------
//  <copyright file="RetryHelperTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Helpers
{
    using System;
    using Pentagon.Helpers;
    using Xunit;

    public class RetryHelperTests
    {
        [Fact]
        public void RetryOnException_Throws()
        {
            Assert.Throws<ArgumentException>(() => RetryHelper.RetryOnException(Callback,
                                                                                3,
                                                                                TimeSpan.FromMilliseconds(200),
                                                                                ExceptionPredicate,
                                                                                TimeSpan.FromMilliseconds(10),
                                                                                TimeSpan.FromMilliseconds(230)));
            void Callback()
            {
                throw new ArgumentException("Something is wrong.");
            }

            bool ExceptionPredicate(Exception arg1, RetryHelper.RetryContext arg2)
            {
                return arg1 is FormatException;
            }
        }
    }
}