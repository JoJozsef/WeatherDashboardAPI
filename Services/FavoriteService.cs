using WeatherDashboardAPI.Data;
using WeatherDashboardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WeatherDashboardAPI.Services
{
    public class FavoriteService
    {
        private readonly WeatherDbContext _context;

        public FavoriteService(WeatherDbContext context)
        {
            _context = context;
        }
        public async Task<List<FavoriteCity>> GetAllAsync()
        {
            return await _context.FavoriteCities.ToListAsync();
        }

        public async Task<FavoriteCity> AddAsync(string cityName) 
        { 
            var favorite = new FavoriteCity { CityName = cityName, AddedAt = DateTime.UtcNow };
            _context.FavoriteCities.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task<bool> DeleteAsync(string cityName) 
        {
            var favorite = await _context.FavoriteCities.FirstOrDefaultAsync(x => x.CityName == cityName);
            if (favorite == null) { return false; }
            _context.FavoriteCities.Remove(favorite);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
