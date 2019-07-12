﻿// -----------------------------------------------------------------------
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
            Assert.Throws<ArgumentException>(() => RetryHelper.RetryOnException(3,
                                                                                TimeSpan.FromMilliseconds(200),
                                                                                Callback,
                                                                                ExceptionPredicate,
                                                                                ForceThrowCallback,
                                                                                TimeSpan.FromMilliseconds(10),
                                                                                TimeSpan.FromMilliseconds(230)));
            void Callback()
            {
                throw new ArgumentException("Something is wrong.");
            }

            bool ForceThrowCallback(Exception arg)
            {
                return arg is FormatException;
            }

            bool ExceptionPredicate(Exception arg1, RetryHelper.RetryContext arg2)
            {
                return true;
            }
        }
    }
}