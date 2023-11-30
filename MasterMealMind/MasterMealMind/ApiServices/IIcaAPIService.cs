using MasterMealMind.API.Models;
using Newtonsoft.Json;
using System.Text;

namespace MasterMealMind.Web.ApiServices
{
    public interface IIcaAPIService
    {
        Task<string> GetAuthenticationTicket(string username, string password);

        Task<RecipeResult> GetRecipes();

        Task<SingleRecipe> GetOneRecipe();
    }
}
