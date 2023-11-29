using MasterMealMind.Models;

namespace MasterMealMind.Management
{
    public class GroceryManagement
    {
        public static Grocery GroceryToUpdate(List<Grocery> groceries, Grocery updatedGrocery)
        {
            var groceryToUpdate = groceries.FirstOrDefault(g => string.Equals(g.Name, updatedGrocery.Name, StringComparison.OrdinalIgnoreCase));
            groceryToUpdate.Name = updatedGrocery.Name;
            groceryToUpdate.Quantity = updatedGrocery.Quantity;
            groceryToUpdate.Description = updatedGrocery.Description;
            
            return groceryToUpdate;
        }
    }
}
