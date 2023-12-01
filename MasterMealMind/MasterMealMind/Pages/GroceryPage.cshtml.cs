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

        public GroceryPageModel(ILocalAPIService localAPIService)
        {
            _localAPIService = localAPIService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Groceries = await _localAPIService.HttpGetGroceriesRequest() ?? new List<Grocery>();

			if (TempData.ContainsKey("EditedGrocery"))
				NewGrocery = JsonConvert.DeserializeObject<Grocery>((string)TempData["EditedGrocery"]);
            
			return Page();
        }
        public async Task<IActionResult> OnPostAddGrocery()
        {
            Groceries = await _localAPIService.HttpGetGroceriesRequest() ?? new List<Grocery>();

            if (NewGrocery != null && NewGrocery.Name != null && !Groceries.Any(g => g.Name == NewGrocery.Name))
                await _localAPIService.HttpPostGrocery(NewGrocery);
            else if (NewGrocery != null && NewGrocery.Name != null && Groceries.FirstOrDefault(g => string.Equals(g.Name, NewGrocery.Name, StringComparison.OrdinalIgnoreCase)) is not null)
            {
                var updatedGrocery = GroceryService.GroceryToUpdate(Groceries, NewGrocery);
                await _localAPIService.HttpUpdateGrocery(updatedGrocery);
            }
            //else
            //    throw new ArgumentNullException(nameof(NewGrocery));


            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteGrocery([FromForm] int deleteId)
        {
            await _localAPIService.HttpDeleteGrocery(deleteId.ToString());


            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostEditGrocery([FromForm] int editId)
        {
            var editedGrocery = await _localAPIService.HttpGetOneGroceryRequest(editId.ToString());
            NewGrocery = editedGrocery;
            TempData["EditedGrocery"] = JsonConvert.SerializeObject(NewGrocery);
			

			return RedirectToPage();
		}

		public async Task<IActionResult> OnPostAddToIngredientSearchList([FromForm] string selectedGroceryNames)
		{
            if (selectedGroceryNames is null)
                return RedirectToPage();

            //         var selectedNames = selectedGroceryNames.Split(',').ToList();
            //         GroceryService.SetIngredientSearchList(selectedNames);


            //Groceries = await _localAPIService.HttpGetGroceriesRequest() ?? new List<Grocery>();

            //foreach (var grocery in Groceries.Where(p => selectedNames.Any(id => id == p.Id)))
            //{
            //	if (ingredientSearchList.Count == 0 || !ingredientSearchList.Contains(grocery.Name))
            //	{
            //		ingredientSearchList.Add(grocery.Name);
            //	}
            //}




            return Page();
		}
	}
}
