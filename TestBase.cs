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

        Api = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = BaseUrl,
            // If the API ever requires headers/auth later, add here.
            // ExtraHTTPHeaders = new Dictionary<string, string> { ["X-Api-Key"] = "..." }
        });
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (Api is not null) await Api.DisposeAsync();
        Playwright?.Dispose();
    }
}