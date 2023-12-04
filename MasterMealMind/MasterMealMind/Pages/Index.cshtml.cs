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

		public RecipeResult RecipeResult { get; set; }


		public IndexModel(ILocalAPIService localAPIService, IcaAPIService icaAPIService)
        {
            _localAPIService = localAPIService;
            _icaAPIService = icaAPIService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            RecipeResult = await _icaAPIService.GetRecipes();
            return Page();
        }
    }
}