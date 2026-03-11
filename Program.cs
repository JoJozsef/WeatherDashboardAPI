using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherDashboardAPI.Services;
using WeatherDashboardAPI.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseSqlite("Data Source=weather.db"));
builder.Services.AddSingleton<WeatherService>();
builder.Services.AddScoped<FavoriteService>();
builder.Services.AddScoped<WeatherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
// Get endpoint
app.MapGet("/weather/{city}", async (string city, [FromServices] WeatherService service) =>
{
    var weatherData = await service.GetWeatherAsync(city);
    if (weatherData == null) return Results.NotFound();
    return Results.Ok(weatherData);
});

// Get favorites
app.MapGet("/weather/{favorite}", async (FavoriteService favorite) =>
{
    return await favorite.GetAllAsync();
});

// Post favorites
app.MapPost("/weather/{favorite}", async () =>
{
    
});

// Delete favorites
app.MapDelete("/weather/{favorite}", async () =>
{

});

app.Run();

public partial class Program { }