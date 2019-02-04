// -----------------------------------------------------------------------
//  <copyright file="DtoPropertyTests.cs">
//   Copyright (c) Michal Pokorný. All Rights Reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Pentagon.Common.Tests
{
    using Xunit;

    public class DtoPropertyTests
    {
        [Fact]
        public void Constructor_Default_CreatesUndefinedProperty()
        {
            var property = new DtoProperty<int>();

            Assert.False(property.IsDefined);

            Assert.Equal(0, property.Value);
        }

        [Fact]
        public void Constructor_WithParameter_CreatesDefinedProperty()
        {
            var property = new DtoProperty<int>(50);

            Assert.True(property.IsDefined);

            Assert.Equal(50, property.Value);
        }

        [Fact]
        public void AsUndefined_ModifyPropertyAsUndefined()
        {
            var property = new DtoProperty<int>(50);

            property.AsUndefined();

            Assert.False(property.IsDefined);
        }

        [Fact]
        public void CastOperator_ConvertsValue()
        {
            var property = new DtoProperty<int>(50);

            int innerValue = property;

            Assert.Equal(50, innerValue);
        }
    }
}