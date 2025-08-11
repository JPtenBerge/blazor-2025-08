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













