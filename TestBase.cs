// NUnit.Framework zapewnia atrybuty do definiowania testów i metod konfiguracji testów, 
// czyli [OneTimeSetUp], [OneTimeTearDown] itp.
using Microsoft.Playwright;
using NUnit.Framework;

namespace PolandApi.ApiTests;

// TestBase  - abstrakcyjna klasa bazowa dla testów API, centralizująca setup i teardown
// nie będzie bezpośrednio instancjonowana
public abstract class TestBase
{
    // 2 pola protected - ważne, aby klasy dziedziczące miały do nich dostępale by nie były publiczne
    // null ! - operator wyłączenia ostrzeżeń dot. null (te pola będą inicjalizowane w OneTimeSetup)
    protected IPlaywright Playwright = null!;
    protected IAPIRequestContext Api = null!;

    private const string BaseUrl = "https://local-gov-units.polandapi.com";

// [OneTimeSetUp] - atrybut NUnit (metadana)
// mówi, że metoda ma być uruchomiona raz przed wszystkimi testami w klasie
// async - tzn. że to metoda asynchroniczna
//    więc pozwala na wykonywanie operacji, które mogą zająć trochę czasu (np. sieciowe)

// CreateAsync() - metoda statyczna do tworzenia instancji Playwright asynchronicznie
//      inicjalizacja pola Playwright które będzie używane do interakcji z API
// APIRequestNewContextOptions - klasa do tworzenia kontekstu żądań API

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Api = await Playwright.APIRequest.NewContextAsync(
            new APIRequestNewContextOptions
            {
                BaseURL = BaseUrl,
                IgnoreHTTPSErrors = true   // ignorujemy błędy certyfikatów SSL
            }
        );
    }
// [OneTimeTearDown] - atrybut NUnit
// mówi, że metoda ma być uruchomiona raz po wszystkich testach w klas
// Task to reprezentacja asynchronicznej operacji, która może zakończyć się sukcesem lub błędem
// zawiera o tym informację (succces czy error) i pozwala na oczekiwanie na jej zakończenie

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        if (Api is not null)
            await Api.DisposeAsync();

        Playwright?.Dispose();
    }
}

// DisposeAsync() vs Dispose() 
// - DisposeAsync() jest metodą asynchroniczną, pozwala uwolnić zasoby w sposób nieblokujący,
//  co jest ważne przy operacjach sieciowych, czasem czasochłonnych