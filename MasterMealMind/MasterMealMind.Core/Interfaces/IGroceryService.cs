using MasterMealMind.Core.Models;

namespace MasterMealMind.Core.Services
{
    public interface IGroceryService
    {
		Task<List<Grocery>> GetAllGroceriesAsync();

        Task<Grocery> GetOneGroceryAsync(int id);

        Task AddOrUpdateGroceryAsync(Grocery grocery);


        Task UpdateGroceryAsync(Grocery updatedGrocery);

        Task DeleteGroceryAsync(int id);

        Task<bool> GroceryExistsAsync(int id);
        Task<bool> GroceryExistsAsync(string name);

        Grocery GetGroceryToUpdate(Grocery grocery, Grocery originalGrocery);
    }
}
