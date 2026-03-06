using System.Net;
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
    public async Task GetOrders_Returns200()
    {
        var response = await _client.GetAsync("/api/v1/Order/GetOrders?page=1&pageSize=10");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetOrders_ReturnsJsonWithData()
    {
        var response = await _client.GetAsync("/api/v1/Order/GetOrders?page=1&pageSize=5");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.True(json.TryGetProperty("succeeded", out var succeeded) && succeeded.GetBoolean());
        Assert.True(json.TryGetProperty("data", out _));
    }
}
