using Microsoft.AspNetCore.Mvc;
using WeatherDashboardAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherService>();

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

app.Run();

public partial class Program { }