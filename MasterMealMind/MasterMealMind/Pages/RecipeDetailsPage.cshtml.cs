using MasterMealMind.API.Models;
using MasterMealMind.Web.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterMealMind.Web.Pages
{
    public class RecipeDetailsPageModel : PageModel
    {
        private readonly IcaAPIService _icaAPIService;

        public Recipe Recipe { get; set; }
        public RecipeDetailsPageModel(IcaAPIService icaAPIService)
        {
            _icaAPIService = icaAPIService;
        }
        public async Task<IActionResult> OnGetAsync(int recipeId)
        {
            Recipe = await _icaAPIService.GetOneRecipe(recipeId);
            return Page();
        }
    }
}