using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.CommentsApi.Core.Interfaces;
using PhotoSharingApplication.CommentsApi.Infrastructure.Data;
using PhotoSharingApplication.CommentsApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CommentsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CommentsDbContext")));

builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication()
        .AddJwtBearer(options =>
        {
            options.Authority = "https://localhost:5001";
            options.TokenValidationParameters.ValidateAudience = false;
        });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
