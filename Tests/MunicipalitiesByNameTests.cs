using System.Text.Json;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PolandApi.ApiTests;

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
        // Arrange
        var url = $"/api/v1/municipalities/name/{Uri.EscapeDataString(name)}";

        // Act
        var response = await Api.GetAsync(url);
        var body = await response.TextAsync();

        // Assert (status)
        Assert.That(response.Ok, Is.True, $"HTTP failed. Status: {(int)response.Status} Body: {body}");

        // Assert (JSON contains success:true and data)
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