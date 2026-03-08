namespace WeatherDashboardAPI.Models
{
    public class WeatherData
    {
        public string City { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public string Description {  get; set; } = string.Empty;
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public DateTime FetchedAt { get; set; }
    }
}
