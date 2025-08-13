using BlazorAppMetInteractiviteit.Client.Repositories;
using Demo.Shared.Repositories;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddTransient<IDestinationRepository, DestinationRepository>();

await builder.Build().RunAsync();


//var client = new Hatseflats.Dinges.demoproject__v1Client(null!);

