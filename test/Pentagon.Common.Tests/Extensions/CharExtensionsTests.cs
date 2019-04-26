namespace Pentagon.Common.Tests.Extensions {
    using System.Linq;
    using Pentagon.Extensions;
    using Xunit;

    public class CharExtensionsTests
    {
        [Fact]
        public void IsHexDigit_ForValidHexCharacters_ReturnsTrue()
        {
            var validChars = new char[] {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'A', 'b', 'B', 'C', 'c', 'd', 'D', 'E', 'e', 'f', 'F'};

            Assert.True(validChars.All(c => CharExtensions.IsHexDigit(c)));
        }

        [Fact]
        public void IsHexDigit_ForInvalidHexCharacters_ReturnsFalse()
        {
            var validChars = new char[] { 'g', 'h', 'p', '.', ']' };

            Assert.True(validChars.All(c => !c.IsHexDigit()));
        }
    }
}