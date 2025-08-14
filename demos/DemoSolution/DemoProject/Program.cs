using Demo.Shared.Repositories;
using DemoProject.Components;
using DemoProject.DataAccess;
using DemoProject.Endpoints;
using DemoProject.Repositories;
using Duende.Bff.Blazor;
using Duende.Bff.Yarp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Scalar.AspNetCore;

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

// BFF setup for blazor
builder.Services.AddBff()
    .AddServerSideSessions() // Add in-memory implementation of server side sessions
    .AddBlazorServer();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddBffExtensions();

builder.Services.AddScoped<IDestinationRepository, DestinationDbRepository>();

builder.Services.AddMudServices();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddOpenApi();

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("blazorfrontend", policy =>
//    {
//        policy.WithOrigins("https://domeina.nl").AllowAnyHeader().AllowCredentials().AllowAnyMethod();
//    });
//});

// Configure the authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";
})
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";

        // Because we use an identity server that's configured on a different site
        // (duendesoftware.com vs localhost), we need to configure the SameSite property to Lax. 
        // Setting it to Strict would cause the authentication cookie not to be sent after loggin in.
        // The user would have to refresh the page to get the cookie.
        // Recommendation: Set it to 'strict' if your IDP is on the same site as your BFF.
        options.Cookie.SameSite = SameSiteMode.Lax;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";
        options.ClientId = "cooleblazorapp";
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
        options.MapInboundClaims = false;

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        //options.Scope.Add("api");
        options.Scope.Add("offline_access");

        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
    });

// Make sure authentication state is available to all components. 
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication(); // leest cookie uit

//app.UseCors("blazorfrontend");
// Add the BFF middleware which performs anti forgery protection
app.UseBff();
app.UseAuthorization();
app.UseAntiforgery();

app.MapReverseProxy();

// Add the BFF management endpoints, such as login, logout, etc.
app.MapBffManagementEndpoints();

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
