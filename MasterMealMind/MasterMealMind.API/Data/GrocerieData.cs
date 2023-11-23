using MasterMealMind.Models;

namespace MasterMealMind.API.Data
{
    public class GrocerieData
    {
        public static List<Grocerie> Ingredients { get; set; }

        public static List<Grocerie> GetDummyIngredients()
        {

            Ingredients = new List<Grocerie>();
            CreateIngredients();
            return Ingredients;
        }

        private static void CreateIngredients()
        {
            Ingredients.Add(new Grocerie { Name = "Gurka",Quantity = 3 } );
            Ingredients.Add(new Grocerie { Name = "Köttfärs", Quantity = 3 });
            Ingredients.Add(new Grocerie { Name = "Potatis", Quantity = 3 });
            Ingredients.Add(new Grocerie { Name = "Lax", Quantity = 3 });
            Ingredients.Add(new Grocerie { Name = "Makaroner", Quantity = 3 });
        }
    }
}
