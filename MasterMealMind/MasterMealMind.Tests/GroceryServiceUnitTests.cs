using MasterMealMind.Core.Models;
using System.Net;
using System.Text.Json;
using Moq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MasterMealMind.Web.Pages;
using MasterMealMind.Web.ApiServices;
using MasterMealMind.Infrastructure.Services;
using MasterMealMind.Core.Interfaces;
using MasterMealMind.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MasterMealMind.Tests
{
	public class GroceryServiceUnitTests
	{
        
		[Fact]
		public void GroceryToUpdate_ShouldUpdateExistingGrocery()
		{
			// Arrange
			var originalGrocery = new Grocery { Id = 1, Name = "Tomato", Quantity = 2, Description = "Red", Unit = Core.Enum.UnitType.st, Storage = Core.Enum.Storage.Kylsk�p };
			var updatedGrocery = new Grocery { Id = 1, Name = "Tomato", Quantity = 3, Description = "Red", Unit = Core.Enum.UnitType.st, Storage = Core.Enum.Storage.Kylsk�p };

			var options = new DbContextOptionsBuilder<MyDbContext>()
				.UseInMemoryDatabase(databaseName: "InMemoryDatabase")
				.Options;

			using (var dbContext = new MyDbContext(options))
			{
				var sut = new GroceryService(dbContext);

				// Act
				var result = sut.GetGroceryToUpdate(updatedGrocery, originalGrocery);

				// Assert
				Assert.NotNull(result);
				Assert.Equal(updatedGrocery.Name, result.Name, StringComparer.OrdinalIgnoreCase);
				Assert.Equal(updatedGrocery.Quantity, result.Quantity);
				Assert.Equal(updatedGrocery.Description, result.Description);
				Assert.Equal(updatedGrocery.Unit, result.Unit);
				Assert.Equal(updatedGrocery.Storage, result.Storage);
			}
		}

		[Fact]
		public async Task AddOrUpdateGrocery_ExistingGrocery_ShouldUpdateToDb()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<MyDbContext>()
				.UseInMemoryDatabase(databaseName: "InMemoryDatabase")
				.Options;

			using (var dbContext = new MyDbContext(options))
			{
				var existingGrocery = new Grocery { Name = "ExistingGrocery" };
				dbContext.Groceries.Add(existingGrocery);
				dbContext.SaveChanges();

				var sut = new GroceryService(dbContext);

				// Act
				var modifiedGrocery = new Grocery { Name = "ExistingGrocery", Quantity = 5 };
				await sut.AddOrUpdateGrocery(modifiedGrocery);

				// Assert
				var updatedGrocery = await dbContext.Groceries.FirstOrDefaultAsync(g => g.Name == "ExistingGrocery");
				Assert.NotNull(updatedGrocery);
				Assert.Equal(5, updatedGrocery.Quantity);
			}
		}

		[Fact]
		public async Task AddOrUpdateGrocery_NewGrocery_ShouldAddToDb()
		{
			// Arrange
			var options = new DbContextOptionsBuilder<MyDbContext>()
				.UseInMemoryDatabase(databaseName: "InMemoryDatabase")
				.Options;

			using (var dbContext = new MyDbContext(options))
			{
				var sut = new GroceryService(dbContext);

				// Act
				var newGrocery = new Grocery { Name = "NewGrocery", Quantity = 10 };
				await sut.AddOrUpdateGrocery(newGrocery);

				// Assert
				var addedGrocery = await dbContext.Groceries.FirstOrDefaultAsync(g => g.Name == "NewGrocery");
				Assert.NotNull(addedGrocery);
				Assert.Equal(10, addedGrocery.Quantity);
			}
		}

	//	[Fact]
	//	public async Task DeleteGrocery_ExistingGrocery_ShouldRemoveFromDatabase()
	//	{
	//		// Arrange
	//		var options = new DbContextOptionsBuilder<MyDbContext>()
	//			.UseInMemoryDatabase(databaseName: "InMemoryDatabase")
	//			.Options;

	//		using (var dbContext = new MyDbContext(options))
	//		{
	//			// L�gg till en befintlig Grocery i in-memory-databasen f�r testet
	//			var existingGrocery = new Grocery { Id = 1, Name = "ExistingGrocery" };
	//			dbContext.Groceries.Add(existingGrocery);
	//			dbContext.SaveChanges();

	//			var sut = new GroceryService(dbContext);

	//			// Act
	//			await sut.DeleteGrocery(1);

	//			// Assert
	//			var deletedGrocery = await dbContext.Groceries.FindAsync(1);
	//			Assert.Null(deletedGrocery);
	//		}
	//	}

	//	[Fact]
	//	public async Task DeleteGrocery_NonExistingGrocery_ShouldNotThrowException()
	//	{
	//		// Arrange
	//		var options = new DbContextOptionsBuilder<MyDbContext>()
	//			.UseInMemoryDatabase(databaseName: "InMemoryDatabase")
	//			.Options;

	//		using (var dbContext = new MyDbContext(options))
	//		{
	//			var sut = new GroceryService(dbContext);

	//			// Act and Assert
	//			await Assert.ThrowsAsync<InvalidOperationException>(() => sut.DeleteGrocery(1));
	//		}
	//	}
	}
}