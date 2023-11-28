using MasterMealMind.Models;
using System.Text.Json;

namespace MasterMealMind.Services
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private const string Base_Address = "https://localhost:44338/api/";

        public HttpService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Base_Address)
            };
        }

        public async Task<List<Grocery>> HttpGetGroceriesRequest(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Grocery>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<List<Recipe>> HttpGetRecipesRequest(string requestUri)
        {
            var response = await _httpClient.GetAsync(requestUri);
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Recipe>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> HttpPostGrocerie(Grocery grocery)
        {
            var jsonString = JsonSerializer.Serialize(grocery);
            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("Grocery", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> HttpDeleteRequest(string requestUri)
        {
            var response = await _httpClient.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }

        public async Task<bool> HttpUpdateRequest<T>(string requestUri, T entity)
        {
            var jsonString = JsonSerializer.Serialize<T>(entity);
            var content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(requestUri, content);
            if (!response.IsSuccessStatusCode)
                return false;
            return true;
        }
    }
}
