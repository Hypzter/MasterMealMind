using MasterMealMind.API.Models;
using MasterMealMind.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MasterMealMind.Web.ApiServices;
using System.Linq;

namespace MasterMealMind.Web.Pages
{
    public class GroceryPageModel : PageModel
    {
        private readonly ILocalAPIService _localAPIService;
        public List<Grocery> Groceries { get; set; }

        [BindProperty]
        public Grocery NewGrocery { get; set; }
		public Grocery EditGrocery { get; set; }


		public GroceryPageModel(ILocalAPIService localAPIService)
        {
            _localAPIService = localAPIService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Groceries = await _localAPIService.HttpGetGroceriesAsync() ?? new List<Grocery>();

			if (TempData.ContainsKey("EditedGrocery"))
				NewGrocery = JsonConvert.DeserializeObject<Grocery>((string)TempData["EditedGrocery"]);

            else
                NewGrocery = new Grocery();
            
			return Page();
        }
        public async Task<IActionResult> OnPostAddGrocery()
        {
            Groceries = await _localAPIService.HttpGetGroceriesAsync() ?? new List<Grocery>();

            if (NewGrocery != null && NewGrocery.Name != null && !Groceries.Any(g => g.Name == NewGrocery.Name))
                await _localAPIService.HttpPostGroceryAsync(NewGrocery);

            else if (NewGrocery != null && NewGrocery.Name != null && Groceries.FirstOrDefault(g => string.Equals(g.Name, NewGrocery.Name, StringComparison.OrdinalIgnoreCase)) is not null)
            {
                var updatedGrocery = GroceryService.GroceryToUpdate(Groceries, NewGrocery);
                await _localAPIService.HttpUpdateGroceryAsync(updatedGrocery);
            }


            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteGrocery([FromForm] int deleteId)
        {
            if (await _localAPIService.HttpGetOneGroceryAsync(deleteId.ToString()) is null)
                return RedirectToPage();

            await _localAPIService.HttpDeleteGroceryAsync(deleteId.ToString());
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostEditGrocery([FromForm] int editId)
        {
            var editedGrocery = await _localAPIService.HttpGetOneGroceryAsync(editId.ToString());
            if (editedGrocery is null)
                return RedirectToPage();

            EditGrocery = editedGrocery;
            TempData["EditedGrocery"] = JsonConvert.SerializeObject(EditGrocery);
			

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostAddToIngredientSearchList([FromForm] string selectedGroceryNames)
		{
            if (selectedGroceryNames is null)
                return RedirectToPage();

			GroceryService.SetIngredientSearch(selectedGroceryNames);
            return RedirectToPage("/Index");
		}
	}
}
