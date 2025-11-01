using Nancy.Json;
using ShopTARge24.Core.Dto.AccuWeather;
using ShopTARge24.Core.Dto.WeatherWebClientDto;
using ShopTARge24.Core.ServiceInterface;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ShopTARge24.Core.Dto.OpenWeather;

namespace ShopTARge24.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        private readonly IConfiguration _cfg;

        public WeatherForecastServices(IConfiguration cfg)
        {
            _cfg = cfg;
        }

        private string AccuApiKey =>
            _cfg["AccuWeather:ApiKey"] ?? throw new InvalidOperationException("AccuWeather API key missing");

        private string OpenWeatherApiKey =>
            _cfg["OpenWeather:ApiKey"] ?? throw new InvalidOperationException("OpenWeather API key missing");

        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            var apiKey = AccuApiKey;

            var baseUrl = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync($"{dto.CityCode}?apikey={apiKey}&details=true");
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var weatherData = JsonSerializer.Deserialize<List<AccuCityCodeRootDto>>(jsonResponse);

                dto.CityName = weatherData![0].LocalizedName;
                dto.CityCode = weatherData[0].Key;
            }

            string weatherResponse =
                $"https://dataservice.accuweather.com/forecasts/v1/daily/1day/{dto.CityCode}?apikey={apiKey}&metric=true";

            using (var clientWeather = new HttpClient())
            {
                var httpResponseWeather = await clientWeather.GetAsync(weatherResponse);
                string jsonWeather = await httpResponseWeather.Content.ReadAsStringAsync();

                var weatherRootDto = JsonSerializer.Deserialize<AccuLocationRootDto>(jsonWeather)!;

                dto.EffectiveDate = weatherRootDto.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDto.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDto.Headline.Severity;
                dto.Text = weatherRootDto.Headline.Text;
                dto.Category = weatherRootDto.Headline.Category;
                dto.EndDate = weatherRootDto.Headline.EndDate;
                dto.EndEpochDate = weatherRootDto.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDto.Headline.MobileLink;
                dto.Link = weatherRootDto.Headline.Link;

                dto.DailyForecastsDate = weatherRootDto.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDto.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDto.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMinUnit = weatherRootDto.DailyForecasts[0].Temperature.Minimum.Unit;
                dto.TempMinUnitType = weatherRootDto.DailyForecasts[0].Temperature.Minimum.UnitType;

                dto.TempMaxValue = weatherRootDto.DailyForecasts[0].Temperature.Maximum.Value;
                dto.TempMaxUnit = weatherRootDto.DailyForecasts[0].Temperature.Maximum.Unit;
                dto.TempMaxUnitType = weatherRootDto.DailyForecasts[0].Temperature.Maximum.UnitType;

                dto.DayIcon = weatherRootDto.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDto.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherRootDto.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayPrecipitationType = weatherRootDto.DailyForecasts[0].Day.PrecipitationType;
                dto.DayPrecipitationIntensity = weatherRootDto.DailyForecasts[0].Day.PrecipitationIntensity;

                dto.NightIcon = weatherRootDto.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDto.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherRootDto.DailyForecasts[0].Night.HasPrecipitation;
            }

            return dto;
        }

        public async Task<AccuLocationWeatherResultDto> AccuWeatherResultWebClient(AccuLocationWeatherResultDto dto)
        {
            var apiKey = AccuApiKey;

            string url =
                $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={apiKey}&q={dto.CityName}";

            using (var client = new WebClient())
            {
                string json = client.DownloadString(url);
                var accuResult = new JavaScriptSerializer()
                    .Deserialize<List<AccuLocationRootWebClientDto>>(json)!;

                dto.CityName = accuResult[0].LocalizedName;
                dto.CityCode = accuResult[0].Key;
            }

            string urlWeather =
                $"https://dataservice.accuweather.com/forecasts/v1/daily/1day/{dto.CityCode}?apikey={apiKey}&metric=true";

            using (var client = new WebClient())
            {
                string json = client.DownloadString(urlWeather);
                var weatherRootDto = new JavaScriptSerializer()
                    .Deserialize<AccuWeatherRootWebClientDto>(json)!;

                dto.EffectiveDate = weatherRootDto.Headline.EffectiveDate;
                dto.EffectiveEpochDate = weatherRootDto.Headline.EffectiveEpochDate;
                dto.Severity = weatherRootDto.Headline.Severity;
                dto.Text = weatherRootDto.Headline.Text;
                dto.Category = weatherRootDto.Headline.Category;
                dto.EndDate = weatherRootDto.Headline.EndDate;
                dto.EndEpochDate = weatherRootDto.Headline.EndEpochDate;

                dto.MobileLink = weatherRootDto.Headline.MobileLink;
                dto.Link = weatherRootDto.Headline.Link;

                dto.DailyForecastsDate = weatherRootDto.DailyForecasts[0].Date;
                dto.DailyForecastsEpochDate = weatherRootDto.DailyForecasts[0].EpochDate;

                dto.TempMinValue = weatherRootDto.DailyForecasts[0].Temperature.Minimum.Value;
                dto.TempMaxValue = weatherRootDto.DailyForecasts[0].Temperature.Maximum.Value;

                dto.DayIcon = weatherRootDto.DailyForecasts[0].Day.Icon;
                dto.DayIconPhrase = weatherRootDto.DailyForecasts[0].Day.IconPhrase;
                dto.DayHasPrecipitation = weatherRootDto.DailyForecasts[0].Day.HasPrecipitation;
                dto.DayPrecipitationType = weatherRootDto.DailyForecasts[0].Day.PrecipitationType;
                dto.DayPrecipitationIntensity = weatherRootDto.DailyForecasts[0].Day.PrecipitationIntensity;

                dto.NightIcon = weatherRootDto.DailyForecasts[0].Night.Icon;
                dto.NightIconPhrase = weatherRootDto.DailyForecasts[0].Night.IconPhrase;
                dto.NightHasPrecipitation = weatherRootDto.DailyForecasts[0].Night.HasPrecipitation;
            }

            return dto;
        }

        // ← siia sisse, sama klassi sisse!
        public async Task<OpenWeatherViewModel> OpenWeatherResult(string city)
        {
            var apiKey = OpenWeatherApiKey;

            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(json);
                var root = doc.RootElement;

                var name = root.GetProperty("name").GetString();

                var main = root.GetProperty("main");
                var temp = main.GetProperty("temp").GetDouble();
                var feels = main.GetProperty("feels_like").GetDouble();
                var pressure = main.GetProperty("pressure").GetInt32();
                var humidity = main.GetProperty("humidity").GetInt32();

                var wind = root.GetProperty("wind");
                var windMs = wind.GetProperty("speed").GetDouble();
                var windKmh = windMs * 3.6;

                string condition = "";
                if (root.TryGetProperty("weather", out var weatherArr) && weatherArr.GetArrayLength() > 0)
                {
                    condition = weatherArr[0].GetProperty("main").GetString();
                }

                return new OpenWeatherViewModel
                {
                    City = name ?? city,
                    TemperatureC = temp,
                    FeelsLikeC = feels,
                    Humidity = humidity,
                    Pressure = pressure,
                    WindSpeedKmh = windKmh,
                    WeatherCondition = condition ?? ""
                };
            }
        }
    }
}
