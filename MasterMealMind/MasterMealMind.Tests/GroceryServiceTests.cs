using MasterMealMind.Core.Models;
using MasterMealMind.Core.Services;
using MasterMealMind.Infrastructure.Services;
using MasterMealMind.Web.ApiServices;
using MasterMealMind.Web.Pages;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace MasterMealMind.Tests
{
    public class GroceryServiceTests : IClassFixture<WebApplicationFactory<API.Program>>
    {
        private readonly HttpClient _httpClient;
        public GroceryServiceTests(WebApplicationFactory<API.Program> factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }

        //[Fact]
        //public void GroceryToUpdate_ShouldUpdateExistingGrocery()
        //{
        //    // Arrange
        //    var originalGrocery = new Grocery { Id = 1, Name = "Tomato", Quantity = 2, Description = "Red", Unit = Core.Enum.UnitType.st, Storage = Core.Enum.Storage.Kylskåp };
            

        //    var updatedGrocery = new Grocery { Id = 1, Name = "Tomato", Quantity = 3, Description = "Red", Unit = Core.Enum.UnitType.st, Storage = Core.Enum.Storage.Kylskåp };

        //    // Act
        //    var result = _groceryService.GetGroceryToUpdate(updatedGrocery, originalGrocery);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Equal(updatedGrocery.Name, result.Name, StringComparer.OrdinalIgnoreCase);
        //    Assert.Equal(updatedGrocery.Quantity, result.Quantity);
        //    Assert.Equal(updatedGrocery.Description, result.Description);
        //    Assert.Equal(updatedGrocery.Unit, result.Unit);
        //    Assert.Equal(updatedGrocery.Storage, result.Storage);
        //}

        [Fact]
        public async Task CanGetListOfAllGroceries()
        {
            var response = await _httpClient.GetAsync("api/groceries");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content.Headers.ContentType?.MediaType);
            Assert.True(response.Content.Headers.ContentLength > 0);
        }
    }
}
