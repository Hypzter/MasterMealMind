using MasterMealMind.Models;
using MasterMealMind.Services;
using System.Net;
using System.Text.Json;
using Moq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MasterMealMind.Pages;

namespace MasterMealMind.Tests
{
    public class UnitTest1
    {
        //[Fact]
        //public async Task HttpGetGroceriesRequest_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    var httpClientMock = new Mock<HttpClient>();
        //    var httpService = new HttpService(httpClientMock.Object);

        //    var expectedGroceries = new List<Grocery> { /* fyll i med exempeldata */ };
        //    var jsonResult = JsonSerializer.Serialize(expectedGroceries);
        //    var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent(jsonResult)
        //    };

        //    httpClientMock.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(httpResponse);

        //    // Act
        //    var result = await httpService.HttpGetGroceriesRequest("example");

        //    // Assert
        //    Assert.Equal(expectedGroceries, result);
        //}

        //[Fact]
        //public async Task HttpGetGroceriesRequest_ReturnsExpectedResult()
        //{
        //    // Arrange
        //    var httpServiceMock = new Mock<IHttpService>();

        //    var expectedGroceries = new List<Grocery> { /* fyll i med exempeldata */ };
        //    httpServiceMock.Setup(service => service.HttpGetGroceriesRequest(It.IsAny<string>())).ReturnsAsync(expectedGroceries);

        //    // Använd mockobjektet i testet
        //    var myClassUnderTest = new Grocery(httpServiceMock.Object); // MyClass antar att den har en IHttpService i konstruktorn

        //    // Act
        //    var result = await myClassUnderTest.MyMethod(); // Anta att MyMethod använder IHttpService

        //    // Assert
        //    // ...assertions här...
        //}

        [Fact]
        public async Task OnPostAddGrocery_WhenGroceryDoesNotExist_HttpPostGroceryCalled()
        {
            // Arrange
            var httpServiceMock = new Mock<HttpService>();
            var pageModel = new GroceryPageModel(httpServiceMock.Object);

            var existingGroceries = new List<Grocery>
        {
            new Grocery { Id = 1, Name = "ExistingGrocery", Description = "EG", Quantity = 2 }
        };

            pageModel.Groceries = existingGroceries; // Simulerar befintliga livsmedel på sidan

            var newGrocery = new Grocery { Id = 2, Name = "NewGrocery", Description = "NG", Quantity = 1 };

            // Act
            await pageModel.OnPostAddGrocery();

            // Assert
            httpServiceMock.Verify(service => service.HttpPostGrocery(newGrocery), Times.Once);
            httpServiceMock.Verify(service => service.HttpUpdateGrocery(It.IsAny<Grocery>()), Times.Never);
        }

    }
}