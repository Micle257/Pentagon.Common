// -----------------------------------------------------------------------
//  <copyright file="RequireResultTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------
namespace Pentagon.Common.Tests
{
    using System;
    using Xunit;

    public class RequireResultTests
    {
        [Fact]
        public void IsValid_ExceptionIsNull_IsTrue()
        {
            var result= new RequireResult<Exception>();

            Assert.True(result.IsValid);
        }
    }
}