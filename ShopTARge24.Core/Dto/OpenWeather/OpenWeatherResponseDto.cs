namespace ShopTARge24.Core.Dto.OpenWeather
{
    public class OpenWeatherResponseDto
    {
        public string? Name { get; set; }
        public List<WeatherInfo>? Weather { get; set; }
        public MainInfo? Main { get; set; }
    }

    public class WeatherInfo
    {
        public string? Description { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }
}
