
using MasterMealMind.Infrastructure.Services;
using MasterMealMind.Core.Models;
using MasterMealMind.Core.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MasterMealMind.Infrastructure.Services
{
    public class GroceryService : IGroceryService
    {
        private readonly MyDbContext _context;
        private static string _ingredientSearch { get; set; }

        public GroceryService(MyDbContext context)
        {
            _context = context;
            _ingredientSearch = string.Empty;
        }

        public static string GetIngredientSearch()
        {
            if (_ingredientSearch is null)
                _ingredientSearch = string.Empty;

            return _ingredientSearch;
        }

        public static void SetIngredientSearch(string ingredientSearch)
        {
            _ingredientSearch = ingredientSearch;
        }

        public static void ClearIngredientSearch()
        {
            _ingredientSearch = string.Empty;
        }

        public async Task<List<Grocery>> GetAllGroceries()
        {
            return await _context.Groceries.ToListAsync();
        }

        public async Task<Grocery> GetOneGrocery(int id)
        {
            var grocery = await _context.Groceries.FirstOrDefaultAsync(g => g.Id == id);

            return grocery != null ? grocery : null;
        }

        public async Task AddOrUpdateGrocery(Grocery modifiedGrocery)
        {
            if (await GroceryExists(modifiedGrocery.Name))
            {
                var existingGrocery = await _context.Groceries.FirstOrDefaultAsync(g => g.Name == modifiedGrocery.Name);
                var updatedExistingGrocery = GetGroceryToUpdate(modifiedGrocery, existingGrocery);
                _context.Entry(updatedExistingGrocery).State = EntityState.Modified;

            }

            else
                await _context.Groceries.AddAsync(modifiedGrocery);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateGrocery(Grocery grocery)
        {

            var groceryToUpdate = await _context.Groceries.FirstOrDefaultAsync(g => string.Equals(g.Name, grocery.Name, StringComparison.OrdinalIgnoreCase)) ?? throw new ArgumentNullException("updateGrocery");
            var updatedGrocery = GetGroceryToUpdate(grocery, groceryToUpdate);
            _context.Entry(updatedGrocery).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void DeleteGrocery(int id)
        {
            var grocery = _context.Groceries.Find(id);

            if (grocery != null)
            {
                _context.Groceries.Remove(grocery);
                _context.SaveChanges();
            }
        }

        public async Task<bool> GroceryExists(int id)
        {
            return await _context.Groceries.AnyAsync(g => g.Id == id);
        }
        public async Task<bool> GroceryExists(string name)
        {
            return await _context.Groceries.AnyAsync(g => g.Name == name);
        }

        public Grocery GetGroceryToUpdate(Grocery updatedGrocery, Grocery groceryToUpdate)
        {
            groceryToUpdate.Name = updatedGrocery.Name;
            groceryToUpdate.Quantity = updatedGrocery.Quantity;
            groceryToUpdate.Description = updatedGrocery.Description;
            groceryToUpdate.Unit = updatedGrocery.Unit;
            groceryToUpdate.Storage = updatedGrocery.Storage;

            return groceryToUpdate;
            
        }
    }
}
