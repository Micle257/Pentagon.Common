// -----------------------------------------------------------------------
//  <copyright file="ExtensionsTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests.Extensions
{
    using System;
    using Pentagon.Extensions;
    using Xunit;

    public class AttributeExtensionsTests
    {
        [Fact]
        public void ShouldIsAttributeReturnTrue()
        {
            var obj = new Foo {Count = 5};
            Assert.Equal(5, obj.Count);
            var result = obj.IsAttribute<BarAttribute>(nameof(obj.Count));
            Assert.True(result);
        }

        [Fact]
        public void ShouldGetPropertyAttributeValue()
        {
            var value = AttributeExtensions.GetPropertyAttributeValue((Foo foo) => foo.Count, (BarAttribute attribute) => attribute.IsActive);

            Assert.True(value);
        }

        [Fact]
        public void ShouldHasAttribute()
        {
            var f = new Foo();

            Assert.True(f.HasAttribute<BarAttribute>());
        }

        [Bar(false)]
        class Foo
        {
            [Bar(true)]
            public int Count { get; set; }
        }

        class BarAttribute : Attribute
        {
            public BarAttribute(bool isActive)
            {
                IsActive = isActive;
            }

            public bool IsActive { get; }
        }
    }
}