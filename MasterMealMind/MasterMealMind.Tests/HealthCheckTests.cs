using Microsoft.AspNetCore.Mvc.Testing;

namespace MasterMealMind.Tests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<API.Program>>
    {

        private readonly HttpClient _httpClient;

        public HealthCheckTests(WebApplicationFactory<API.Program> factory)
        {
            _httpClient = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheckReturnsHealthyAsync()
        {
            var response = await _httpClient.GetAsync("api/healthcheck");

            response.EnsureSuccessStatusCode();
        }
    }
}
