using WeatherDashboardAPI.Models;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;

namespace WeatherDashboardAPI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public WeatherService(HttpClient httpClient, IConfiguration configuration, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _memoryCache = cache;
        }
        public async Task<WeatherData?> GetWeatherAsync(string city) 
        {
            var cacheKey = $"weather_{city.ToLower()}";

            if (_memoryCache.TryGetValue(cacheKey, out WeatherData? cacheWeather))
            {
                return cacheWeather;
            }

            try
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

                var weatherData = new WeatherData
                {
                    City = city,
                    Temperature = temperature,
                    Description = description,
                    Humidity = humidity,
                    WindSpeed = windSpeed,
                    FetchedAt = DateTime.UtcNow
                };

                _memoryCache.Set(cacheKey, weatherData, TimeSpan.FromMinutes(5));

                return weatherData;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
                return null;
            }
            catch (Exception ex) {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
            
        }
    }
}
