using MasterMealMind.API.Models;

namespace MasterMealMind.Web.ApiServices
{
    public interface ILocalAPIService
    {
        Task<List<Grocery>> HttpGetGroceriesRequest();
        Task<Grocery> HttpGetOneGroceryRequest(string requestUri);
		Task<bool> HttpPostGrocery(Grocery grocery);
        Task<bool> HttpDeleteGrocery(string requestUri);
        Task<bool> HttpUpdateGrocery(Grocery grocery);
    }
}
