using System;
using Xunit;
using BattleshipGame;

namespace BattleshipGame.Tests
{
    public class HelperTests
    {
        [Theory]
        [InlineData(0, "A")]
        [InlineData(2, "C")]
        [InlineData(9, "J")]
        [InlineData(25, "Z")]
        [InlineData(42, " ")]
        public void GetRowLetterByNumber_ReturnsExpectedResult(int number, string expectedResult)
        {
            Assert.Equal(Helper.GetRowLetterByNumber(number), expectedResult);
        }

        [Theory]
        [InlineData('A', 0)]
        [InlineData('C', 2)]
        [InlineData('J', 9)]
        public void GetNumberOfLetter_ReturnsExpectedResult(char letter, int expectedResult)
        {
            Assert.Equal(Helper.GetNumberOfLetter(letter), expectedResult);
        }
    }
}
