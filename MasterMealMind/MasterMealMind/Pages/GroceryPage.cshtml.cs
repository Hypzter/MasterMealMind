using MasterMealMind.Models;
using MasterMealMind.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace MasterMealMind.Pages
{
    public class GroceryPageModel : PageModel
    {
        private readonly IHttpService _httpService;
        public List<Grocery> Groceries { get; set; }

        [BindProperty]
        public Grocery NewGrocery { get; set; }

        public GroceryPageModel(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Groceries = await _httpService.HttpGetGroceriesRequest() ?? new List<Grocery>();

			if (TempData.ContainsKey("EditedGrocery"))
				NewGrocery = JsonConvert.DeserializeObject<Grocery>((string)TempData["EditedGrocery"]);
            
			return Page();
        }
        public async Task<IActionResult> OnPostAddGrocery()
        {
            Groceries = await _httpService.HttpGetGroceriesRequest() ?? new List<Grocery>();

            if (NewGrocery != null && NewGrocery.Name != null && !Groceries.Any(g => g.Name == NewGrocery.Name))
                await _httpService.HttpPostGrocery(NewGrocery);
            else if (NewGrocery != null && NewGrocery.Name != null && Groceries.FirstOrDefault(g => string.Equals(g.Name, NewGrocery.Name, StringComparison.OrdinalIgnoreCase)) is not null)
            {
                var updatedGrocery = Management.GroceryManagement.GroceryToUpdate(Groceries, NewGrocery);
                await _httpService.HttpUpdateGrocery(updatedGrocery);
            }
            //else
            //    throw new ArgumentNullException(nameof(NewGrocery));


            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteGrocery([FromForm] int deleteId)
        {
            await _httpService.HttpDeleteGrocery(deleteId.ToString());


            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostEditGrocery([FromForm] int editId)
        {
            var editedGrocery = await _httpService.HttpGetOneGroceryRequest(editId.ToString());
            NewGrocery = editedGrocery;
            TempData["EditedGrocery"] = JsonConvert.SerializeObject(NewGrocery);
			

			return RedirectToPage();
		}
	}
}
