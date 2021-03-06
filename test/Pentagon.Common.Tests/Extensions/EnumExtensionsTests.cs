﻿// -----------------------------------------------------------------------
//  <copyright file="EnumExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using Pentagon.Extensions;
    using Xunit;

    public class EnumExtensionsTests
    {
        enum Stub
        {
            First,
            [Tested]
            Second
        }
        
        [Fact]
        public void GetItemValue_PassedEnumItemWithAttribute_ReturnsNotNullAttribute()
        {
            var item = Stub.Second;

            var result = item.GetItemAttribute<TestedAttribute, Stub>();

            Assert.NotNull(result);
        }

        [Fact]
        public void GetItemValue_PassedEnumItemWithoutAttribute_ReturnsNull()
        {
            var item = Stub.First;

            var result = item.GetItemAttribute<TestedAttribute, Stub>();

            Assert.Null(result);
        }

        class TestedAttribute : Attribute { }
    }
}