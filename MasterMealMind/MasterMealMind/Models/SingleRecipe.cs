using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MasterMealMind
{

    public class SingleRecipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ImageId { get; set; }
        public object YouTubeId { get; set; }
        public Ingredientgroup[] IngredientGroups { get; set; }
        public string PreambleHTML { get; set; }
        public object CurrentUsersRating { get; set; }
        public string AverageRating { get; set; }
        public string Difficulty { get; set; }
        public string CookingTime { get; set; }
        public int Portions { get; set; }
    }

    public class Ingredientgroup
    {
        public string GroupName { get; set; }
        public Ingredient[] Ingredients { get; set; }
    }

    public class Ingredient
    {
        public string Text { get; set; }
        public int IngredientId { get; set; }
        public double Quantity { get; set; }
        public object Unit { get; set; }
        [JsonPropertyName("Ingredient")]
        public string Name { get; set; }
    }

}

