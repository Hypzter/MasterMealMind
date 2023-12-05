using MasterMealMind.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterMealMind.API.Services
{
    public interface IGroceryService
    {
		Task<List<Grocery>> GetAllGroceries();

        Task<Grocery> GetOneGrocery(int id);

        Task CreateGrocery(Grocery grocery);


        Task UpdateGrocery(Grocery updatedGrocery);


        void DeleteGrocery(int id);

        Task<bool> GroceryExists(int id);
    }
}
