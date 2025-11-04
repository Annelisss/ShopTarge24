using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Models.OpenWeather
{
    public class OpenWeatherViewModel
    {
        [Required]
        [Display(Name = "City")]
        public string? City { get; set; }

        public string? CityName { get; set; }
        public string? Description { get; set; }
        public double? TempC { get; set; }
        public int? Humidity { get; set; }
        public double? FeelsLike { get; set; }
        public int? Pressure { get; set; }
        public double? WindSpeed { get; set; }

    }
}
