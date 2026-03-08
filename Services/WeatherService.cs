using WeatherDashboardAPI.Models;
using System.Text.Json;

namespace WeatherDashboardAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<WeatherData?> GetWeatherAsync(string city) 
        {
            var apiKey = _configuration["OpenWeatherMap:ApiKey"];
            var baseUrl = _configuration["OpenWeatherMap:BaseUrl"];

            var url = $"{baseUrl}/weather?q={city}&appid={apiKey}&units=metric";

            var jsonResponse = await _httpClient.GetStringAsync(url);

            using var doc = JsonDocument.Parse(jsonResponse);
            var root = doc.RootElement;

            var temperature = root.GetProperty("main").GetProperty("temp").GetDouble();
            var humidity = root.GetProperty("main").GetProperty("humidity").GetInt32();
            var description = root.GetProperty("weather")[0].GetProperty("description").GetString() ?? "N/A";
            var windSpeed = root.GetProperty("wind").GetProperty("speed").GetDouble();

            return new WeatherData
            {
                City = city,
                Temperature = temperature,
                Description = description,
                Humidity = humidity,
                WindSpeed = windSpeed,
                FetchedAt = DateTime.UtcNow
            };
        }
    }
}
