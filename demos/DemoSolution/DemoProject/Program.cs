using DemoProject.Components;
using DemoProject.DataAccess;
using Demo.Shared.Repositories;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using DemoProject.Repositories;
using DemoProject.Endpoints;
using Scalar.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<DemoContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext"));
//}, ServiceLifetime.Transient);

builder.Services.AddDbContextFactory<DemoContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoContext"));
});

builder.Services.AddScoped<IDestinationRepository, DestinationDbRepository>();

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddOpenApi();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorAppMetInteractiviteit.Client._Imports).Assembly);

app.MapDestinationEndpoints();

app.Run();
