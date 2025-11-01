namespace ShopTARge24.Models.OpenWeathers
{
    public class OpenWeatherViewModel
    {
        public string City { get; set; } = "";
        public double TemperatureC { get; set; }
        public double FeelsLikeC { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeedKmh { get; set; }
        public string WeatherCondition { get; set; } = "";
    }
}
