using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace MT.Api.IntegrationTests;

public class OrderApiTests : IClassFixture<MerchTradeApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderApiTests(MerchTradeApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetOrders_WithoutAuth_Returns401()
    {
        var response = await _client.GetAsync("/api/v1/Order/GetOrders?page=1&pageSize=10");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetOrders_AsStaff_Returns200()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Order/GetOrders?page=1&pageSize=10");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "test-admin");
        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrders_ReturnsJsonWithData()
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/v1/Order/GetOrders?page=1&pageSize=5");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "test-admin");
        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.True(json.TryGetProperty("succeeded", out var succeeded) && succeeded.GetBoolean());
        Assert.True(json.TryGetProperty("data", out _));
    }
}
