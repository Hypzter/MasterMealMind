using MasterMealMind.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMealMind.Tests
{
	public class ProfanityFilterServiceTests
	{
		[Theory]
		[InlineData("This is a test", "This is a test")]
		[InlineData("jävla test", "**** test")]
		[InlineData("idiot and jävel", "**** and ****")]
		[InlineData("NoProfanityHere", "NoProfanityHere")]
		[InlineData("", "")]                               
		[InlineData(null, null)]                           
		public void FilterProfanity_ShouldFilterCorrectly(string input, string expectedOutput)
		{
			// Arrange
			var sut = new ProfanityFilterService();

			// Act
			string result = sut.FilterProfanity(input);

			// Assert
			Assert.Equal(expectedOutput, result);
		}
	}
}
