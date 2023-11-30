namespace MasterMealMind.API.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int ImageId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public object PreambleHTML { get; set; }
        public string CookingTime { get; set; }
        public int CookingTimeMinutes { get; set; }
        public float AverageRating { get; set; }
        public int CommentCount { get; set; }
        public int IngredientCount { get; set; }
        public int OfferCount { get; set; }
    }
    public class RecipeResult
    {
        public int NumberOfPages { get; set; }
        public List<Recipe> Recipes { get; set; }
        public int TotalNumberOfRecipes { get; set; }
        public string Msg { get; set; }
    }
}
