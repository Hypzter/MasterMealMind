using MasterMealMind.Models;
using MasterMealMind.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterMealMind.Pages
{
    public class GroceryPageModel : PageModel
    {
        private readonly HttpService _httpService;
        public List<Grocery> Groceries { get; set; }

        [BindProperty]
        public Grocery NewGrocery { get; set; }

        public GroceryPageModel(HttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (Groceries == null) 
            {
                Groceries = await _httpService.HttpGetGroceriesRequest("Grocery");
            }
            return Page();
        }
        public async Task<IActionResult> AddGroceryOnPost()
        {
            if (NewGrocery != null && NewGrocery.Name != null)
            {
                await _httpService.HttpPostGrocerie(NewGrocery);
            }

            return RedirectToPage();
        }
    }
}
