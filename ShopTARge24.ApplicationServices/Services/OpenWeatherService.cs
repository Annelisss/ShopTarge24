using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ShopTARge24.Core.Dto.OpenWeather;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class OpenWeatherService : IOpenWeatherService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public OpenWeatherService(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public async Task<OpenWeatherResponseDto?> GetCurrentWeather(string city)
        {
 
            var apiKey = _config["OpenWeather:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
                return null;

  
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var result = JsonSerializer.Deserialize<OpenWeatherResponseDto>(json, options);
            return result;
        }
    }
}
