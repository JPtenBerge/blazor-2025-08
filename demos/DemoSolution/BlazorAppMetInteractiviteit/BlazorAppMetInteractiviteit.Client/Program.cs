using BlazorAppMetInteractiviteit.Client.Repositories;
using Demo.Shared.Repositories;
using Duende.Bff.Blazor.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddBffBlazorClient() // Provides auth state provider that polls the /bff/user endpoint
    .AddCascadingAuthenticationState();

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddTransient<IDestinationRepository, DestinationRepository>();

await builder.Build().RunAsync();


//var client = new Hatseflats.Dinges.demoproject__v1Client(null!);

