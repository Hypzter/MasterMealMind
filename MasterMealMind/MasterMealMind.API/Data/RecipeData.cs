using MasterMealMind.Models;

namespace MasterMealMind.API.Data
{
    public class RecipeData
    {
        public static List<Recipe> Recipes { get; set; }

        public static List<Recipe> GetDummyRecipes()
        {

            Recipes = new List<Recipe>();
            CreateRecipe();
            return Recipes;
        }

        private static void CreateRecipe()
        {
            var ingredients = Data.GrocerieData.GetDummyIngredients();
            Recipes.Add(new Recipe { Name = "Laxsallad", Description = "Hacka laxen, stek den, blanda med sallad", Ingredients = ingredients.Where(x => x.Name == "Lax").ToList() });
            Recipes.Add(new Recipe { Name = "Köttfärssås", Description = "Hacka laxen, stek den, blanda med sallad", Ingredients = ingredients.Where(x => x.Name == "Köttfärs").ToList() });
            Recipes.Add(new Recipe { Name = "Potatisplättar", Description = "Hacka laxen, stek den, blanda med sallad", Ingredients = ingredients.Where(x => x.Name == "Potatis").ToList() });
            Recipes.Add(new Recipe { Name = "Taco", Description = "Hacka laxen, stek den, blanda med sallad", Ingredients = ingredients.Where(x => x.Name == "Gurka").ToList() });
            Recipes.Add(new Recipe { Name = "Makaronerilåda", Description = "Hacka laxen, stek den, blanda med sallad", Ingredients = ingredients.Where(x => x.Name == "Makaroner").ToList() });
        }
    }
}
