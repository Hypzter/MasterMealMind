using MasterMealMind.Core.Models;

namespace MasterMealMind.Core.Services
{
    public interface IGroceryService
    {
		Task<List<Grocery>> GetAllGroceries();

        Task<Grocery> GetOneGrocery(int id);

        Task AddOrUpdateGrocery(Grocery grocery);


        Task UpdateGrocery(Grocery updatedGrocery);


        Task DeleteGrocery(int id);

        Task<bool> GroceryExists(int id);
        Task<bool> GroceryExists(string name);

        Grocery GetGroceryToUpdate(Grocery grocery, Grocery originalGrocery);
    }
}
