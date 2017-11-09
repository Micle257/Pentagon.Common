using System;

namespace Pentagon.Root.Tests
{
    using Exceptions;
    using Helpers;
    using Xunit;

    public class RequireTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void StringNotNullNorWhiteSpace_ValueIsNotValid_Throws(string value)
        {
            Assert.Throws<StringArgumentException>(() => Require.StringNotNullNorWhiteSpace(value));
        }

        [Theory]
        [InlineData("s")]
        [InlineData("  s  ")]
        public void StringNotNullNorWhiteSpace_ValueIsValid_IsValidIsTrue  (string value)
        {
          var result =  Require.StringNotNullNorWhiteSpace(value).IsValid;

            Assert.True(result);
        }

        [Fact]
        public void NotNull_ValueIsNotNull_IsValidIsTrue()
        {
            var notNull = "";

            var res =  Require.NotNull(() => notNull);

            Assert.Equal(true, res.IsValid);
        }

        [Fact]
        public void NotNull_ValueIsNull_Throws()
        {
            var stub = default(object);

            Require.ThrowExceptions = true;
            Assert.Throws<ArgumentNullException>(() => Require.NotNull(() => stub));
        }

        [Fact]
        public void NotNull_ReturnsRequireResultOfExpectedGenericType()
        {
            var notNull = "";

            var res = Require.NotNull(() => notNull);
            var concrete = res as RequireResult<ArgumentNullException>;

            Assert.NotNull(concrete);
        }

        [Fact]
        public void NotNull_ParameterIsDeclaredImplicitly_ResultExceptionHaveRightParameterName()
        {
            var stub = default(object);
            var name = "";

            try { Require.NotNull(() => stub); }
            // ReSharper disable once UncatchableException
            catch (ArgumentNullException e)
            {
                name = e.ParamName;
            }

            Assert.Equal(nameof(stub), name);
        }

        [Fact]
        public void NotNull_ParameterIsDeclaredExplicitly_ResultExceptionHaveRightParameterName()
        {
            var stub = new TestStruct {Value = null};
            var name = "";

            try { Require.NotNull(() => stub.Value); }
            // ReSharper disable once UncatchableException
            catch (ArgumentNullException e)
            {
                name = e.ParamName;
            }

            Assert.Equal(nameof(stub.Value), name);
        }

        [Fact]
        public void NotDefault_ValueIsDefault_Throws()
        {
            var stub = default(double);

            Assert.Throws<ArgumentException>(() => Require.NotDefault(() => stub));
        }

        [Fact]
        public void NotDefault_ReferenceObjectIsDefault_Throws()
        {
            var stub = default(string);

            Assert.Throws<ArgumentException>(() => Require.NotDefault(() => stub));
        }

        [Fact]
        public void NotDefault_ValueIsNotDefault_IsValidIsTrue()
        {
            var stub = 5d;
            
            var result = Require.NotDefault(() => stub);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsType_ValueTypeDoesntMatchRequiredType_Throws()
        {
            var type = new ChildType();
            
            Assert.Throws<ArgumentException>(() => Require.IsType<string>(type));
        }

        [Fact]
        public void IsType_ValueTypeMatchesRequiredType_IsValidIsTrue()
        {
            var type = new ChildType();
            
          var result =  Require.IsType<IParentType>(type);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(0, 10, 11)]
        [InlineData(0, 10, -1)]
        public void InRange_ValueIsNotInRange_Throws(int min, int max, int value)
        {
            var range = new Range<int>(min, max);

            Assert.Throws<ValueOutOfRangeException<int>>(() => Require.InRange(() => value, range));
        }

        [Theory]
        [InlineData(0, 10, 10)]
        [InlineData(0, 10, 2)]
        public void InRange_ValueIsInRange_IsValidIsTrue(int min, int max, int value)
        {
            var range = new Range<int>(min, max);

            Require.InRange(() => value, range);
        }


        public struct TestStruct
        {
            public object Value { get; set; }
        }

        public interface IParentType { }

        public class ChildType : IParentType { }
    }

}
