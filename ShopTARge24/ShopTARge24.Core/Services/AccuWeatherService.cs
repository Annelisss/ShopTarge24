using System.Text.Json;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.AccuWeathers;

namespace ShopTARge24.Core.Services
{
    public class AccuWeatherService : IAccuWeatherService
    {
        public async Task<AccuWeatherViewModel> AccuWeatherSearch(string city)
        {
            AccuWeatherViewModel dto = new();

            string apikey = "rpak_08c6f3f4a9147e5821_9bf9d9d9";
            string response = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apikey}&q={city}";

            using (var client = new HttpClient())
            {
                var httpResponse = await client.GetAsync(response);
                string json = await httpResponse.Content.ReadAsStringAsync();

                List<AccuCityCodeRootDto>? weatherData =
                    JsonSerializer.Deserialize<List<AccuCityCodeRootDto>>(json);

                if (weatherData != null && weatherData.Count > 0)
                {
                    dto.CityName = weatherData[0].LocalizedName ?? "Unknown";
                }
                else
                {
                    dto.CityName = "City not found";
                }
            }

            return dto;
        }
    }
}
