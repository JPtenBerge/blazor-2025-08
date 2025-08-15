using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PhotoSharingApplication;
using PhotoSharingApplication.Client.Core.Interfaces;
using PhotoSharingApplication.Client.Pages;
using PhotoSharingApplication.Components;
using PhotoSharingApplication.Core.Interfaces;
using PhotoSharingApplication.Infrastructure.Data;
using PhotoSharingApplication.Infrastructure.Repositories;
using System.IdentityModel.Tokens.Jwt;
using Yarp.ReverseProxy.Transforms;

const string MS_OIDC_SCHEME = "MicrosoftOidc";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<PhotosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PhotosDbContext")));

builder.Services.AddScoped<IPhotosRepository, PhotosEFRepository>();

builder.Services.AddHttpForwarder();

builder.Services.AddHttpClient<ICommentsRepository, CommentsRepository>(httpClient =>
{
    httpClient.BaseAddress = new("https://localhost:7123");
});

// COPIED FROM OidcSamples and adjusted for this application to work with Duende IdentityServer
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = MS_OIDC_SCHEME;
})
    .AddOpenIdConnect(MS_OIDC_SCHEME, oidcOptions =>
     {
         oidcOptions.Scope.Add("comments");

         oidcOptions.Authority = "https://localhost:5001";

         oidcOptions.ClientId = "photosharingapplication";
         oidcOptions.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
         oidcOptions.ResponseType = OpenIdConnectResponseType.Code;
         
         oidcOptions.MapInboundClaims = false;
         
         oidcOptions.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
         oidcOptions.TokenValidationParameters.RoleClaimType = "role";

         oidcOptions.GetClaimsFromUserInfoEndpoint = true;
     })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.ConfigureCookieOidcRefresh(CookieAuthenticationDefaults.AuthenticationScheme, MS_OIDC_SCHEME);

builder.Services.AddAuthorization();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<AuthenticationStateProvider, PersistingAuthenticationStateProvider>();

builder.Services.AddHttpContextAccessor();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
} else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PhotoSharingApplication.Client._Imports).Assembly);

app.MapForwarder("/api/{**catch-all}", "https://localhost:7123");
app.MapForwarder("/apiauth/{**catch-all}", "https://localhost:7123", transformBuilder =>
{
    transformBuilder.AddRequestTransform(async transformContext =>
    {
        var accessToken = await transformContext.HttpContext.GetTokenAsync("access_token");
        transformContext.ProxyRequest.Headers.Authorization = new("Bearer", accessToken);
    });
}).RequireAuthorization();

app.MapGroup("/authentication").MapLoginAndLogout();

app.Run();
