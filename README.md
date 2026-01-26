### Jakie jest przeznaczenie tego projektu ?

Definiuje i uruchamia zestaw automatycznych testów API dla serwisu local-gov-units.polandapi.com. Testy wykonują zapytania HTTP (GET/POST itd.) przez Playwright APIRequestContext i weryfikują odpowiedzi (statusy, payload, zachowanie endpointów).

**english abstract**
*It defines and runs an automated API test suite for local-gov-units.polandapi.com. It sends HTTP requests using Playwright’s APIRequestContext and asserts responses**

## Wymagania (prerequisites)

.NET SDK 8.0

Dostęp do internetu / endpointów API

Zainstalowane zależności NuGet (restore wykona się automatycznie przy dotnet test)

Uwaga: jeśli środowisko testowe ma wygasły / nieprawidłowy certyfikat TLS, w projekcie może być użyte IgnoreHTTPSErrors = true w konfiguracji kontekstu API (jak w TestBase.cs).

## Jak uruchomić testy ? (how to run tests ?)

Z katalogu repozytorium (lub folderu z solution/projektem):

dotnet test PolandApi.ApiTests/PolandApi.ApiTests.csproj


Opcjonalnie, z większą ilością logów:

dotnet test PolandApi.ApiTests/PolandApi.ApiTests.csproj -v normal

## Co można ulepszyć? (what can be improved ?)

- Konfiguracja przez appsettings / zmienne środowiskowe (BaseURL, flagi TLS, timeouts)

- Lepsze asercje kontraktu: np. walidacja JSON Schema

- Raportowanie: TRX / HTML report, fragmenty logu z odpowiedziami z usługi

- Pokrycie negatywne: więcej testów błędów (404/400), walidacje, edge-case’y, testy bezpieczeństwa (np. nagłówki)

## Jak można rozszerzyć projekt? (what are possible extentions ?)

- Dodać testy dla kolejnych zasobów (np. województwa/powiaty/gminy, wyszukiwarki, paginacja)

- Dodać warstwę „client SDK”/wrapper na API, aby testy były krótsze i czytelniejsze

- Uruchamianie w CI (np. GitHub Actions/Azure DevOps)

- dodać publikację raportów
