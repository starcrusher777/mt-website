using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using MerchTrade.Controllers;

namespace MT.Api.IntegrationTests;

public class MerchTradeApplicationFactory : WebApplicationFactory<AuthController>
{
    public MerchTradeApplicationFactory()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
    }
}
