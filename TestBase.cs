using Microsoft.Playwright;
using NUnit.Framework;

namespace PolandApi.ApiTests;

public abstract class TestBase
{
    protected IPlaywright Playwright = null!;
    protected IAPIRequestContext Api = null!;

    private const string BaseUrl = "https://local-gov-units.polandapi.com";

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Api = await Playwright.APIRequest.NewContextAsync(
            new APIRequestNewContextOptions
            {
                BaseURL = BaseUrl,
                IgnoreHTTPSErrors = true   // ✅ FIX: ignore expired / invalid TLS cert
            }
        );
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (Api is not null)
            await Api.DisposeAsync();

        Playwright?.Dispose();
    }
}