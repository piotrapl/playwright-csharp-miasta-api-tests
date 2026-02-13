using System.Text.Json;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PolandApi.ApiTests;

// using - dyrektywa do importowania przestrzeni nazw, umożliwiająca korzystanie z klas bez konieczności pełnego kwalifikowania ich nazw
// System.Text.Json - przestrzeń z klasami do pracy z danymi w formacie JSON
// Microsoft.Playwright - przestrzeń z klasami do automatyzacji przeglądarek
// NUnit.Framework - przestrzeń z klasami do pisania testów jednostkowych w NUnit 

// PolandApi.ApiTests - przestrzeń nazw dla testów API dotyczących polskich jednostek samorządu teryt.

// PositiveNames() - metoda zwracająca kolekcję nazw miast do testów pozytywnych
// yield return - słowo kluczowe do zwracania pojedynczych elementów kolekcji w metodzie iteratora
// IEnumerable<TestCaseData> - kolekcja danych testowych dla NUnit
// [TestFixture] - atrybut NUnit
// oznacza że ta klasa jest fiksturą (fixture) testową NUnit czyli klasą zawierającą testy
public class MunicipalitiesByNameTests : TestBase
{
    public static IEnumerable<TestCaseData> PositiveNames()
    {
        yield return new TestCaseData("Lodz")
            .SetName("GET municipalities by name - Lodz");
        yield return new TestCaseData("Szczecin")
            .SetName("GET municipalities by name - Szczecin");
    }

    [TestCaseSource(nameof(PositiveNames))]
    public async Task Get_municipalities_by_name_should_return_success_true_and_data(string name)
    {
        // Przygotowanie
        var url = $"/api/v1/municipalities/name/{Uri.EscapeDataString(name)}";

        // Wykonanie: Api.GetAsync(url) - metoda do wysyłania żądania GET do określonego URL
        var response = await Api.GetAsync(url);
        var body = await response.TextAsync();

        // Weryfikacja czy status odpowiedzi HTTP jest OK (200)
        Assert.That(response.Ok, Is.True, $"HTTP failed. Status: {(int)response.Status} Body: {body}");

        // Weryfikacja czy odpowiedź JSON zawiera pole success ustawione na true oraz blok data
        // using var - deklaracja zmiennej lokalnej z automatycznym zarządzaniem zasobami
        //   using var ponieważ JsonDocument implementuje IDisposable i wymaga uwolnienia zasobów
        using var doc = JsonDocument.Parse(body);
        var root = doc.RootElement;

        Assert.That(
            root.TryGetProperty("success", out var success) && success.ValueKind == JsonValueKind.True,
            Is.True,
            $"Expected JSON field success=true. Body: {body}"
        );

        Assert.That(
            root.TryGetProperty("data", out var data) && data.ValueKind != JsonValueKind.Null,
            Is.True,
            $"Expected JSON block data to exist (and not be null). Body: {body}"
        );
    }
}