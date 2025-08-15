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
        NameClaimType = JwtClaimTypes.Name,
    };

    // HTTP header Authorization: Bearer <jwt>
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("alleencoolemensen", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(JwtClaimTypes.Name, "Bob Smith");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    Console.WriteLine($"JWT:\r\n{context.Request.Headers.Authorization}");
    await next(context);
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
