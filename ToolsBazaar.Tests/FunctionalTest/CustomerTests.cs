using System.Net;
using FluentAssertions;

namespace ToolsBazaar.Tests.FunctionalTest
{
    public class CustomersScenarios : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public CustomersScenarios(TestWebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task GetTopFiveCustomers_ReturnsOKStatusCode()
        {
            var response = await _httpClient.GetAsync("/customers/top");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNull();
        }
    }
}