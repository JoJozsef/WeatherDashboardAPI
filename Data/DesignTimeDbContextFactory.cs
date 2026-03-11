using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WeatherDashboardAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WeatherDbContext> 
    {
        public WeatherDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>();
            optionsBuilder.UseSqlite("Data Source=weather.db");

            return new WeatherDbContext(optionsBuilder.Options);
        }
    }
}
