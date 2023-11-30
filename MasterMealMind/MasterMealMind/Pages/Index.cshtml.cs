using MasterMealMind.API.Models;
using MasterMealMind.Web.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace MasterMealMind.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILocalAPIService _localAPIService;
        private readonly IcaAPIService _icaAPIService;



        [BindProperty]
        public Grocery Grocery { get; set; }
        [BindProperty]
        public List<Grocery> Ingredients { get; set; }
        [BindProperty]
        public SingleRecipe Recipe { get; set; }

        public IndexModel(ILocalAPIService localAPIService, IcaAPIService icaAPIService)
        {
            _localAPIService = localAPIService;
            _icaAPIService = icaAPIService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Recipe = await _icaAPIService.GetOneRecipe();
            return Page();
        }
        public async Task<IActionResult> OnPostAddGrocerie() 
        {
            if (Grocery != null && Grocery.Name != null)
            {
                await _localAPIService.HttpPostGrocery(Grocery);
            }
            return RedirectToPage();
        }

    }
}