using MasterMealMind.DAL;
using MasterMealMind.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MasterMealMind.API.DAL
{
    public class GrocerieRepository
    {
        private readonly MyDbContext _context;
        private readonly string _connectionString;

        public GrocerieRepository(MyDbContext context, Connection connectionString)
        {
            _context = context;
            _connectionString = connectionString.GetConnectionString();
        }

        internal async Task<List<Grocery>> GetAllGroceries()
        {
            return await _context.Groceries.ToListAsync();
        }

        internal async Task<Grocery> GetOneGrocerie(int id)
        {
            var grocerie = await _context.Groceries.FirstOrDefaultAsync(g => g.Id == id);

            return grocerie != null ? grocerie : null;
        }

        internal async Task CreateGrocerie(Grocery grocerie)
        {
            var newGrocerie = new Grocery
            {
                Name = grocerie.Name,
                Description = grocerie.Description,
                Quantity = grocerie.Quantity
            };
            await _context.Groceries.AddAsync(newGrocerie);
            await _context.SaveChangesAsync();
        }
    }
}
