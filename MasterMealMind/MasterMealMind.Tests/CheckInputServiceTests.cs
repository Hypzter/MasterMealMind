using MasterMealMind.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealMind.Tests
{
    public class CheckInputServiceTests
    {
        [Theory]
        [InlineData("TestString", 20, true)]
        [InlineData("StringThatShouldReturnFalse", 20, false)]
        [InlineData("", 10, false)] // Testar null eller tom sträng
        [InlineData(null, 10, false)] // Testar null
        [InlineData("NegativeValue", -5, false)] // Testar med ett negativt maxNumberOfDigits
        [InlineData("Four", 4, true)] // Testar exakt längd
        public void IsInputLengthValid_ShouldReturnExpectedResult(string input, int maxNumberOfDigits, bool expected)
        {
            // Arrange
            var sut = new CheckInputService();

            // Act
            var result = sut.IsInputLengthValid(input, maxNumberOfDigits);

            // Assert
            Assert.Equal(result, expected);
        }

    }
}
