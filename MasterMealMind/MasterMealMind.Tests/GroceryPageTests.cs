using MasterMealMind.Models;
using MasterMealMind.Services;
using System.Net;
using System.Text.Json;
using Moq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MasterMealMind.Pages;
using MasterMealMind.API.DAL;

namespace MasterMealMind.Tests
{
    public class GroceryPageTests
    {
        //[Fact]
        //public async Task HttpGetGroceriesRequest_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    var httpClientMock = new Mock<HttpClient>();
        //    var httpService = new HttpService(httpClientMock.Object);

        //    var expectedGroceries = new List<Grocery>
        //    {
        //        new Grocery { Id = 1, Name = "ExistingGrocery", Description = "EG", Quantity = 2 }
        //    };

        //    var jsonResult = JsonSerializer.Serialize(expectedGroceries);
        //    var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(jsonResult)
        //    };

        //    httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponse);

        //    // Act
        //    var result = await httpService.HttpGetGroceriesRequest();

        //    // Assert
        //    Assert.Equal(expectedGroceries, result);
        //}

        [Fact]
        public async Task OnPostAddGrocery_WhenGroceryDoesNotExist_HttpPostGroceryCalled()
        {
            // Arrange
            var httpServiceMock = new Mock<IHttpService>();
            var pageModel = new GroceryPageModel(httpServiceMock.Object);

            var existingGroceries = new List<Grocery>
            {
                new Grocery { Id = 1, Name = "ExistingGrocery", Description = "EG", Quantity = 2 }
            };

            pageModel.Groceries = existingGroceries;

            pageModel.NewGrocery = new Grocery { Id = 2, Name = "NewGrocery", Description = "NG", Quantity = 1 };

            // Act
            await pageModel.OnPostAddGrocery();

            // Assert
            httpServiceMock.Verify(s => s.HttpPostGrocery(pageModel.NewGrocery), Times.Once);
            httpServiceMock.Verify(service => service.HttpUpdateGrocery(It.IsAny<Grocery>()), Times.Never);

        }

        [Fact]
        public async Task OnPostAddGrocery_WhenGroceryDoesExist_HttpUpdateGroceryCalled()
        {
            // Arrange
            var httpServiceMock = new Mock<IHttpService>();
            var existingGroceries = new List<Grocery>
            {
                new Grocery { Id = 1, Name = "ExistingGrocery", Description = "EG", Quantity = 2 }
            };

            httpServiceMock.Setup(s => s.HttpGetGroceriesRequest()).ReturnsAsync(existingGroceries);

            var pageModel = new GroceryPageModel(httpServiceMock.Object);

            pageModel.NewGrocery = new Grocery { Id = 2, Name = "ExistingGrocery", Description = "EG", Quantity = 1 };


            // Act
            await pageModel.OnPostAddGrocery();

            // Assert
            httpServiceMock.Verify(service => service.HttpUpdateGrocery(It.IsAny<Grocery>()), Times.Once);
            httpServiceMock.Verify(s => s.HttpPostGrocery(pageModel.NewGrocery), Times.Never);
        }
    }
}