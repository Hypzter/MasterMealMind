using MasterMealMind.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterMealMind.API.DAL
{
    public interface IGroceryRepository
    {
        Task<List<Grocery>> GetAllGroceries();

        Task<Grocery> GetOneGrocery(int id);

        Task CreateGrocery(Grocery grocery);


        Task UpdateGrocery(Grocery updatedGrocery);


        void DeleteGrocery(int id);

        Task<bool> GroceryExists(int id);
    }
}
