using Microsoft.EntityFrameworkCore;
using WeatherDashboardAPI.Models;

namespace WeatherDashboardAPI.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext (DbContextOptions<WeatherDbContext> options) : base(options) { }

        public DbSet<FavoriteCity> FavoriteCities { get; set; }
    }
}
