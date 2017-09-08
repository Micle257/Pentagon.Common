using System;

namespace Pentagon.Root.Tests
{
    using System.Runtime.InteropServices;
    using Exceptions;
    using Helpers;
    using Xunit;

    public class RequireTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void Should_StringNotNullNorWhiteSpace_ThrowWhen_StringIsNotValid(string value)
        {
            Assert.Throws<StringArgumentException>(() => Require.StringNotNullNorWhiteSpace(value));
        }

        [Theory]
        [InlineData("s")]
        [InlineData("  s  ")]
        public void Should_StringNotNullNorWhiteSpace_Return_IsValid_True_When_StringIsValid  (string value)
        {
          var result =  Require.StringNotNullNorWhiteSpace(value).IsValid;

            Assert.True(result);
        }

        [Fact]
        public void Should_NotNull_Validate_WhenValueIsNotNull()
        {
            var notNull = "";

            var res =  Require.NotNull(() => notNull);

            Assert.Equal(true, res.IsValid);
        }

        [Fact]
        public void Should_NotNull_Throw_WhenValueIsNull()
        {
            var stub = default(object);

            Require.ThrowExceptions = true;
            Assert.Throws<ArgumentNullException>(() => Require.NotNull(() => stub));
        }

        [Fact]
        public void Should_NotNull_ReturnType_BeOfExpectedType()
        {
            var notNull = "";

            var res = Require.NotNull(() => notNull);
            var concrete = res as RequireResult<ArgumentNullException>;

            Assert.NotNull(concrete);
        }

        [Fact]
        public void Should_NotNull_ResultException_HaveRightName()
        {
            var stub = default(object);
            string name = "";

            try { Require.NotNull(() => stub); }
            catch (ArgumentNullException e)
            {
                name = e.ParamName;
            }

            Assert.Equal(nameof(stub), name);
        }

        [Fact]
        public void Should_NotDefault_ThrowWhen_ValueTypeIsDefault()
        {
            var stub = default(double);

            Assert.Throws<ArgumentException>(() => Require.NotDefault(() => stub));
        }

        [Fact]
        public void Should_NotDefault_ThrowWhen_ReferenceTypeIsDefault()
        {
            var stub = default(string);

            Assert.Throws<ArgumentException>(() => Require.NotDefault(() => stub));
        }

        [Fact]
        public void ShouldRequire_NotDefault_Return_IsValid_True_ForNotDefaultObject()
        {
            var stub = 5d;
            
            var result = Require.NotDefault(() => stub);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void ShouldValidIsType()
        {
            var type = new ChildType();

            Assert.Throws<ArgumentException>(() => Require.IsType<double>(type));
            Assert.Throws<ArgumentException>(() => Require.IsType<string>(type));
            Require.IsType<IParentType>(type);
            Require.IsType<ValueType>(5d);
        }

        [Fact]
        public void ShouldValidValueInRange()
        {
            var range1 = new Range<int>(0, 10);
            var range2 = new Range<TimeSpan>(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(25));

            Assert.Throws<ValueOutOfRangeException<int>>(() => Require.InRange(() => 11, range1));
            Assert.Throws<ValueOutOfRangeException<int>>(() => Require.InRange(() => -1, range1));
            Require.InRange(() => 10, range1);

            Assert.Throws<ValueOutOfRangeException<TimeSpan>>(() => Require.InRange(() => TimeSpan.FromSeconds(26), range2));
            Assert.Throws<ValueOutOfRangeException<TimeSpan>>(() => Require.InRange(() => TimeSpan.FromSeconds(2), range2));
            Require.InRange(() => TimeSpan.FromSeconds(10), range2);
        }

        public struct TestStruct { }

        public interface IParentType { }

        public class ChildType : IParentType { }
    }

}
