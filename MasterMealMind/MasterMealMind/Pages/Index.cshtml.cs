using MasterMealMind.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MasterMealMind.Pages
{
    public class IndexModel : PageModel
    {

        public List<Grocerie> Ingredients { get; set; }
        public List<Recipe> Recipes { get; set; }

        public IndexModel()
        {
            if (Ingredients == null)
            {
                
            }
        }
        public void OnGet()
        {

        }
    }
}