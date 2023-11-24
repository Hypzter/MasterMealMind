using MasterMealMind.Models;
using MasterMealMind.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterMealMind.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpService _httpService;


        [BindProperty]
        public Grocerie Grocerie { get; set; }
        [BindProperty]
        public Recipe Recipe { get; set; } = new Recipe();
        public List<Grocerie> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }

        public IndexModel(HttpService httpService)
        {
            _httpService = httpService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAddGrocerie() 
        {
            if (Grocerie != null && Grocerie.Name != null)
            {
                await _httpService.HttpPostGrocerie(Grocerie);
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddRecipe()
        {
            if (Recipe != null && Recipe.Name != null)
            {
                await _httpService.HttpPostRecipe(Recipe);
            }
            return RedirectToPage();
        }
    }
}