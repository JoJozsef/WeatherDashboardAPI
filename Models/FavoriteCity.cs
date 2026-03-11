namespace WeatherDashboardAPI.Models
{
    public class FavoriteCity
    {
        public int Id { get; set; }
        public string CityName { get; set; } = string.Empty;
        public DateTime AddedAt { get; set; }
    }
}
