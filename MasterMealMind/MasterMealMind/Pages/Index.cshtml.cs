using MasterMealMind.Models;
using MasterMealMind.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MasterMealMind.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpService _httpService;
        private readonly RecipeService _recipeService;



        [BindProperty]
        public Grocery Grocery { get; set; }
        [BindProperty]
        public List<Grocery> Ingredients { get; set; }
        [BindProperty]
        public SingleRecipe Recipe { get; set; }

        public IndexModel(IHttpService httpService, RecipeService recipeService)
        {
            _httpService = httpService;
            _recipeService = recipeService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Recipe = await _recipeService.GetOneRecipe();
            return Page();
        }
        public async Task<IActionResult> OnPostAddGrocerie() 
        {
            if (Grocery != null && Grocery.Name != null)
            {
                await _httpService.HttpPostGrocery(Grocery);
            }
            return RedirectToPage();
        }

    }
}