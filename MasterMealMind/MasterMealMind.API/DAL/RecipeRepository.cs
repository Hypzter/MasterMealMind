using MasterMealMind.DAL;
using MasterMealMind.Models;
using Microsoft.EntityFrameworkCore;

namespace MasterMealMind.API.DAL
{
    public class RecipeRepository
    {
        private readonly MyDbContext _context;
        private readonly string _connectionString;

        public RecipeRepository(MyDbContext context, Connection connectionString)
        {
            _context = context;
            _connectionString = connectionString.GetConnectionString();
        }

        internal async Task<List<Recipe>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        internal async Task<Recipe> GetOneRecipe(int id)
        {
            var grocerie = await _context.Recipes.FirstOrDefaultAsync(g => g.Id == id);

            return grocerie != null ? grocerie : null;
        }

        internal async Task CreateRecipe(Recipe recipe)
        {
            var newRecipe = new Recipe
            {
                Name = recipe.Name,
                Description = recipe.Description,
            };
            await _context.Recipes.AddAsync(newRecipe);
            await _context.SaveChangesAsync();
        }
    }
}
