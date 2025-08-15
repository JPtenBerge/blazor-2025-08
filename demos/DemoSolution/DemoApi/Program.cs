using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Duende.IdentityModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = "https://localhost:5001";
    options.TokenValidationParameters = new()
    {
        ValidateAudience = false,
        ValidateIssuer = true,
        NameClaimType = ClaimTypes.NameIdentifier,
    };

    // HTTP header Authorization: Bearer <jwt>
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("alleencoolemensen", policy =>
    {
        policy.RequireAuthenticatedUser();
        //policy.RequireUserName("2");
        policy.RequireClaim(ClaimTypes.NameIdentifier, "2");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request binnen: {context.Request.QueryString.Value}");

    await next(context);
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
