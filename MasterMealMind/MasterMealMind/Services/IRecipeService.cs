using MasterMealMind.Models;
using Newtonsoft.Json;
using System.Text;

namespace MasterMealMind.Services
{
    public interface IRecipeService
    {
        Task<string> GetAuthenticationTicket(string username, string password);

        Task<RecipeResult> GetRecipes();

        Task<SingleRecipe> GetOneRecipe();
    }
}
