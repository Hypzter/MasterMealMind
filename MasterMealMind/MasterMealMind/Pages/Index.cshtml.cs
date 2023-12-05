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
        private readonly IIcaAPIService _icaAPIService;

		public RecipeResult RecipeResult { get; set; }


		public IndexModel(IIcaAPIService icaAPIService)
        {
            _icaAPIService = icaAPIService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            RecipeResult = await _icaAPIService.GetRecipes();
            return Page();
        }
    }
}