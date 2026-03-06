using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace MT.Api.IntegrationTests;

public class AuthFlowTests : IClassFixture<MerchTradeApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthFlowTests(MerchTradeApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_Returns401()
    {
        var body = JsonContent.Create(new { email = "nonexistent@test.com", password = "wrong" });
        var response = await _client.PostAsync("/api/v1/Auth/Login", body);
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Register_Returns200OrConflict()
    {
        var unique = Guid.NewGuid().ToString("N")[..8];
        var body = JsonContent.Create(new
        {
            username = $"user_{unique}",
            email = $"user_{unique}@test.com",
            password = "TestPassword1!"
        });
        var response = await _client.PostAsync("/api/v1/Auth/Register", body);
        Assert.True(response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Conflict,
            $"Expected 200 or 409, got {response.StatusCode}");
    }
}
