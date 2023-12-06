using MasterMealMind.API.Models;
using System.Net;
using System.Text.Json;
using Moq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MasterMealMind.Web.Pages;
using MasterMealMind.Web.ApiServices;
using MasterMealMind.API.Services;

namespace MasterMealMind.Tests
{
	public class GroceryPageTests
	{
		[Fact]
		public async Task OnPostAddGrocery_WhenGroceryDoesNotExist_HttpPostGroceryCalled()
		{
			// Arrange
			var mockLocalAPIservice = new Mock<ILocalAPIService>();

			var sut = new GroceryPageModel(mockLocalAPIservice.Object);

			var existingGroceries = new List<Grocery>
			{
				new Grocery { Id = 1, Name = "ExistingGrocery", Description = "EG", Quantity = 2 }
			};

			sut.Groceries = existingGroceries;

			sut.NewGrocery = new Grocery { Id = 2, Name = "NewGrocery", Description = "NG", Quantity = 1 };

			// Act
			await sut.OnPostAddOrUpdateGrocery();

			// Assert
			mockLocalAPIservice.Verify(s => s.HttpPostGroceryAsync(sut.NewGrocery), Times.Once);
			mockLocalAPIservice.Verify(service => service.HttpUpdateGroceryAsync(It.IsAny<Grocery>()), Times.Never);
		}

		[Fact]
		public async Task OnPostAddGrocery_WhenGroceryDoesExist_HttpUpdateGroceryCalled()
		{
			// Arrange
			var mockLocalAPIService = new Mock<ILocalAPIService>();

			var existingGroceries = new List<Grocery>
			{
				new Grocery { Id = 1, Name = "ExistingGrocery", Description = "EG", Quantity = 2 }
			};

			mockLocalAPIService.Setup(s => s.HttpGetGroceriesAsync()).ReturnsAsync(existingGroceries);

			var sut = new GroceryPageModel(mockLocalAPIService.Object);

			sut.NewGrocery = new Grocery { Id = 2, Name = "ExistingGrocery", Description = "EG", Quantity = 1 };


			// Act
			await sut.OnPostAddOrUpdateGrocery();

			// Assert
			mockLocalAPIService.Verify(service => service.HttpUpdateGroceryAsync(It.IsAny<Grocery>()), Times.Once);
			mockLocalAPIService.Verify(s => s.HttpPostGroceryAsync(sut.NewGrocery), Times.Never);
		}

		[Fact]
		public async Task OnPostAddGrocery_ShouldAddNewGrocery_WhenNewGroceryIsValid()
		{
			// Arrange
			var mockLocalAPIService = new Mock<ILocalAPIService>();
			var sut = new GroceryPageModel(mockLocalAPIService.Object)
			{
				NewGrocery = new Grocery { Name = "NewGrocery", Quantity = 10 }
			};

			// Act
			await sut.OnPostAddOrUpdateGrocery();

			// Assert
			mockLocalAPIService.Verify(s => s.HttpPostGroceryAsync(It.IsAny<Grocery>()), Times.Once);
		}

		[Fact]
		public async Task OnPostAddGrocery_ShouldUpdateExistingGrocery_WhenNewGroceryExists()
		{
			// Arrange
			var mockLocalAPIService = new Mock<ILocalAPIService>();

			var sut = new GroceryPageModel(mockLocalAPIService.Object)
			{
				Groceries = new List<Grocery> { new Grocery { Id = 1, Name = "ExistingGrocery", Quantity = 10 } },
				NewGrocery = new Grocery { Id = 2, Name = "UpdatedGrocery", Quantity = 20 }
			};

			// Act
			await sut.OnPostAddOrUpdateGrocery();

			// Assert
			mockLocalAPIService.Verify(s => s.HttpPostGroceryAsync(It.IsAny<Grocery>()), Times.Once);
		}
	}
}