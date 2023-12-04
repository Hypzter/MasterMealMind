﻿using MasterMealMind.API.Models;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration.UserSecrets;
using MasterMealMind.API.Services;

namespace MasterMealMind.Web.ApiServices
{
    public class IcaAPIService : IIcaAPIService
    {

        private static readonly string _baseUrl = "https://handla.api.ica.se/";
        private readonly IConfiguration _configuration;
        public IcaAPIService()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddUserSecrets<IcaAPIService>(); // Replace YourClass with the type where you want to access the secrets
            _configuration = configurationBuilder.Build();
        }


        public async Task<string> GetAuthenticationTicket(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {

                // Concatenate username and password for HTTP Basic authentication
                string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);

                // Make a GET request to /api/login
                HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/login/");
                response.EnsureSuccessStatusCode();

                // Extract AuthenticationTicket from the response headers
                string authenticationTicket = response.Headers.GetValues("AuthenticationTicket").FirstOrDefault();

                return authenticationTicket;
            }
        }

        public async Task<RecipeResult> GetRecipes()
        {
            string authenticationTicket = await GetAuthenticationTicket(_configuration["YourConfigKey:Username"], _configuration["YourConfigKey:Password"]);

            using (HttpClient client = new HttpClient())
            {
                RecipeResult result = null;
                var phrase = GroceryService.GetIngredientSearch();

                // Add AuthenticationTicket to the request headers
                client.DefaultRequestHeaders.Add("AuthenticationTicket", authenticationTicket);

                // Make a GET request to /api/recipes/searchwithfilters
                HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/recipes/searchwithfilters?recordsPerPage=40&pageNumber=1&phrase={phrase}&sorting=0");
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response content (JSON)
                string responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<RecipeResult>(responseBody);
                return result;
            }
        }

        public async Task<Recipe> GetOneRecipe(int recipeId)
        {
            string authenticationTicket = await GetAuthenticationTicket(_configuration["YourConfigKey:Username"], _configuration["YourConfigKey:Password"]);

            using (HttpClient client = new HttpClient())
            {
                Recipe result = null;

                // Add AuthenticationTicket to the request headers
                client.DefaultRequestHeaders.Add("AuthenticationTicket", authenticationTicket);

                // Make a GET request to /api/recipes/searchwithfilters
                HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/recipes/recipe/{recipeId}");
                response.EnsureSuccessStatusCode();

                // Read and deserialize the response content (JSON)
                string responseBody = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<Recipe>(responseBody);
                return result;
            }
        }
    }
}