# Notes

## Blazor

- voor het eerst uitgekomen met .NET Core 3.1 - 2019/2020 ergens
- waarom?
  - geen JavaScript, maar C#
  - alles wat je al kent in .NET-wereld kun je nu voor frontend gebruiken
  - Microsoft
  - technische uitblinkers? content projection.
- Namen: Steve Sanderson (OG maker) & Daniel Roth (programmamanager)
- nadeel: HMR  Hot Module Reloading  live refresh  hot reloading  
  - het werkt ongeveer 40% van de tijd
- nadeel: Tailwind

## Paradigmas der webdevelopment

- Single Page Application - SPA
  - libs/frameworks: Blazor Angular React Vue Svelte Qwik Solid Knockout (oud)
  - de browser doet veel meer werk
  - hoog niveau aan interactiviteit, snellere feedback
  - nadeeltje: lastig te crawlen!
  - meer complexiteit
- Server-side rendering (SSR)
  - server rendert pagina?
  - complementair aan de SPA
    - libs/frameworks: ASP.NET Core (Blazor) @angular/ssr Next.js Nuxt.js SvelteKit SolidStart QwikCity
  - de server rendert de initieel opgevraagde pagina
    - crawlers zoals die van Google kunnen dan PRIMA indexeren!
    - op achtergrond ophalen van JS zodat webapp interactief wordt: hydration partial hydration streaming hydration
  - de server rendert (HTML klaarstomen) de initiele pagina
  - overhead? extra lib, extra infrastructuur, extra complexiteit
    - authenticatie/autorisatie
- Multi Page Application (MPA)
  - complexiteit: valt mee.
  - classic
  - ieder klikje is een paginarefresh - server moet nieuwe pagina gaan renderen
  - Flash Of Unstyled Content
  - niet hip

## Blazor-edities

- Blazor WebAssembly
  - interactief
  - WebAssembly is een browser feature - code runnen in browser zonder JavaScript  for if while
  - jouw code draait IN DE BROWSER
    - jouw C# code === compile ==> webassembly
      - heule kleine van .NET meegestuurd naar de browser.
    - geen connectie? niks aan de hand! althans, tot je data op gaat halen van de server.
    - nadeel: security - code is zichtbaar voor users.
    - nadeel: langzamer. op alle vlakken.
    - nadeel: even een kleine versie van .NET moeten downloaden. "Hello world"-Blazor WebAssembly is 7MB
- Blazor Server
  - interactief
  - jouw code draait OP DE SERVER
  - via WebSockets
    - elk klikje en typeje beweginkje input => server. server berekent de nieuwe UI state.
      UI state wordt teruggecommuniceerd naar de browser. En een klein stukje JS rendert 
      die wijzigingen.
    - nadeel: geen connectie? dode UI.
- Blazor Static SSR
  - nauwelijks interactief, klassieke MPA.
  - ASP.NET Core MVC/Razor Pages  doodgaande
  - componentenmodel is fijn
  - in de toekomst alsnog interactiviteit aangaan is relatief eenvoudig

## Geschiedenis

- .NET Framework
  - 2001
  - officieel cross-platform, maar Microsoft leverde enkel een Windows-implementatie
  - 4.7.2
  - geen Blazor
  - WebForms
.NET Core
  - 2016
  - open source
  - cross-platform

.NET Core 5 ====rename==> .NET 5


## a11y

Accessibility: 11 is het aantal karakters tussen de a en y van accessibility.

- i18n - internationalization
- l10n - localization
- k8s kubernetes

a11y:
- "normale" mensen
- slechtzienden/blinden
- cognitieve issues
- crawlers - zoekmachines


## Dependency injection

- managen van lifecycles van objecten
- het is een vorm van Inversion of Control

ASP.NET Core DI:

- `.AddTransient()` - overal een nieuwe
   - middleware controllers repo service service service service  allemaal nieuwe instanties.
- `.AddScoped()` - per request een nieuwe.
  - middleware controllers repo service service service service  `IWhateverService`
- `.AddSingleton()` - 1 instance to rule them all
  - shared state 

Repository
- tussenlaagje tussen wat uiteindelijk je db aanroept
- centrale data access
  - kolom `IsInactive`  true
  - `Where(x => !x.IsInactive)`
- database-onafhankelijkheid
  - EF Core

## Component library installeren

Meestal ongeveer deze stappen doorlopen:

- NuGet package installeren
- globale componenten opnemen in `App.razor`/`MainLayout.razor`
- dependency injection-zaken regelen in `Program.cs`
- global usings `@using MudBlazor`
- `index.html` static assets opnemen - `.css`, fonts, `.js`

En dan klaar om te gaan!

Wees je ook bewust dat niet alle component libraries compatibel zijn met Blazor Static SSR, bijv. MudBlazor en dat er ook een MudBlazor.Static

## Circuit-issues Blazor Server

Dependency injection werkt met deze 3 methoden:

```cs
.AddTransient()
.AddScoped()
.AddSingleton()
```
Per editie van Blazor zit daar verschil in:

- Blazor Static SSR
  - `.AddScoped()` - per request, voorspelbaar!
- Blazor Server
  - `.AddScoped()` - per socketconnectie, specifiek "per SignalR-circuit"
    - SignalR: wrapper om websocket, met als extraatjes dat je berichtjes naar groepen kan sturen en dat hij reconnect als de verbinding wegvalt.
- Blazor WebAssembly
  - `.AddScoped()` - per tabblad zolang de gebruiker gebruikt maakt van de webapp in dat tabblad
    - F5 en alles begint weer opnieuw
    - [is een Singleton](https://learn.microsoft.com/en-us/aspnet/core/blazor/fundamentals/dependency-injection?view=aspnetcore-9.0#service-lifetime)

Bij Blazor Server i.c.m. EF Core treden hier het snelst issues bij op, maar het kan evengoed met andere services gebeuren.

## `HttpClient`/Flurl

HTTP-berichtje vanaf frontend naar server sturen: AJAX (`XMLHttpRequest`/`fetch()`)

`HttpClient` nadeeltjes:
- unittesten, met name mocken. Er is geen `IHttpClient`
- voelt wat lomp met de POST-response te verwerken
  - wel correct. Het ontvangen van de headers en de body kun je apart `await`en
- headers meesturen

Bovenstaande nadeeltjes zijn makkelijk weg te werken met kleine wrappertjes (typed HTTP Client) of zelf een paar extra extension methods te schrijven. Een library als Flurl kan ook.

### CORS: Cross-origin resource sharing

- Dat je niet zomaar van domein A ==> domein B een request mag sturen
- is een security-iets
- wordt enkel in de browser gecheckt
- is enkel bij AJAX want dit mag allemaal wel:
  ```html
  <form action="https://anderdomein.nl">
  <script src="https://anderdomein.nl/script.js"></script>  CDN
  <img src="...">
  <link href="https://googlefonts.com">
  ```

Hoe weet de browser dat een request vanaf domein A gestuurd mag worden?

```text
preflight check  HEAD/OPTIONS <== Allow-Control-Access-Origin: https://domeina.nl
POST ==> domeinb.nl
```

## Blazor rendermodes

Twee projecten, wat plaats je waar?

- DemoProject
  - Blazor Server
  - componenten die NOOIT naar webassembly hoeven te worden gecompileerd.
- DemoProject.Client
  - Blazor WebAssembly
  - componenten die mogelijk naar webassembly moeten te worden gecompileerd.
  - ook bruikbaar voor Blazor Server

## Overig

`Enhance` op een form/routelink plaatsen zodat request via AJAX verstuurd wordt.

AJAX: Asynchronous JavaScript And XML
- berichtje naar backend sturen via JavaScript
- XHR: `XMLHttpRequest`
- `fetch()`

## Coole links

- [Frontend frameworks benchmarks](https://github.com/krausest/js-framework-benchmark)
- [Microsoft die wil dat je `s_` bij `static` members gebruikt](https://learn.microsoft.com/en-us/dotnet/
csharp/fundamentals/coding-style/identifier-names)
  - eigenlijk niet heel cool, maar wel relevant
- [Awesome Blazor](https://github.com/AdrienTorris/awesome-blazor) voor lijstje met toffe CSS/component libraries
 Vet snelle coole site met preloaden als je over producten hovert: https://www.mcmaster.com/