using MasterMealMind.Models;

namespace MasterMealMind.Services
{
    public interface IHttpService
    {
        Task<List<Grocery>> HttpGetGroceriesRequest();
        Task<Grocery> HttpGetOneGroceryRequest(string requestUri);
		Task<bool> HttpPostGrocery(Grocery grocery);
        Task<bool> HttpDeleteGrocery(string requestUri);
        Task<bool> HttpUpdateGrocery(Grocery grocery);
    }
}
