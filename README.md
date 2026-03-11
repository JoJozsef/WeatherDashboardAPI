# Weather Dashboard API

Weather Dashboard REST API built with ASP.NET Core.

## Features

- ✅ Real-time weather data (OpenWeatherMap integration)
- ✅ Error handling
- ✅ Caching (performance optimization)
- ✅ Favorite cities (SQLite database)
- ✅ Security (API key protection)
- ✅ SQLite database with Entity Framework Core

## Tech Stack

- ASP.NET Core 8.0
- Entity Framework Core
- SQLite
- Minimal API architecture
- Swagger/OpenAPI documentation
- OpenWeatherMap API integration

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or VS Code
- OpenWeatherMap API key

### Installation

1. Clone the repository
```bash
git clone https://github.com/JoJozsef/WeatherDashboardAPI.git
cd WeatherDashboardAPI
```

2. Set up API key

Create `appsettings.Development.json`:
```json
{
  "OpenWeatherMap": {
    "ApiKey": "YOUR_API_KEY_HERE"
  }
}
```

3. Run database migrations
```bash
dotnet ef database update
```

4. Run the application
```bash
dotnet run
```

5. Open Swagger UI
```
https://localhost:7266/swagger
```

(Port may vary - check console output)

## API Endpoints

### Weather

- `GET /weather/{city}` - Get current weather data for a city

### Favorites

- `GET /favorites` - Get all favorite cities
- `POST /favorites?city={city}` - Add a city to favorites
- `DELETE /favorites/{city}` - Remove a city from favorites

## Project Structure
```
WeatherDashboardAPI/
├── Data/
│   ├── DesignTimeDbContextFactory.cs
│   └── WeatherDbContext.cs
├── Models/
│   ├── FavoriteCity.cs
│   └── WeatherData.cs
├── Services/
│   ├── FavoriteService.cs
│   └── WeatherService.cs
└── Program.cs
```

## Author

József - [GitHub](https://github.com/JoJozsef)

## License

This project is for educational purposes.