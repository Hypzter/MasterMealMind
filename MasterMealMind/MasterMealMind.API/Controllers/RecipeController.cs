using MasterMealMind.API.DAL;
using MasterMealMind.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MasterMealMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly RecipeRepository _recipeRepository;

        public RecipeController(RecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }


        [HttpGet]
        public async Task<IEnumerable<Models.Recipe>> GetRecipesAsync() => await _recipeRepository.GetAllRecipes();


        [HttpGet("{id}")]
        public async Task<Models.Recipe> GetOneRecipeAsync(int id) => await _recipeRepository.GetOneRecipe(id);


        [HttpPost]
        public async Task CreateRecipeAsync([FromBody] Models.Recipe recipe) => await _recipeRepository.CreateRecipe(recipe);

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
