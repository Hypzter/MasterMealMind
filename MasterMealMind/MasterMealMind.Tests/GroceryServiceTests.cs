using MasterMealMind.API.Models;
using MasterMealMind.API.Services;
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

        [Fact]
        public void GroceryToUpdate_ShouldUpdateExistingGrocery()
        {
            // Arrange
            var groceries = new List<Grocery>
            {
            new Grocery { Id = 1, Name = "Tomato", Quantity = 2, Description = "Red", Unit = API.Enum.UnitType.st, Storage = API.Enum.Storage.Kylskåp },
            new Grocery { Id = 2, Name = "Onion", Quantity = 1, Description = "White", Unit = API.Enum.UnitType.st, Storage = API.Enum.Storage.Skafferi },
            };

            var updatedGrocery = new Grocery { Id = 1, Name = "Tomato", Quantity = 3, Description = "Red", Unit = API.Enum.UnitType.st, Storage = API.Enum.Storage.Kylskåp };

            // Act
            var result = GroceryService.GroceryToUpdate(groceries, updatedGrocery);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedGrocery.Name, result.Name, StringComparer.OrdinalIgnoreCase);
            Assert.Equal(updatedGrocery.Quantity, result.Quantity);
            Assert.Equal(updatedGrocery.Description, result.Description);
            Assert.Equal(updatedGrocery.Unit, result.Unit);
            Assert.Equal(updatedGrocery.Storage, result.Storage);
        }

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
