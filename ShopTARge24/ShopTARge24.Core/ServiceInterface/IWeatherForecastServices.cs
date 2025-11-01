using ShopTARge24.Core.Dto.AccuWeather;
using ShopTARge24.Core.Dto.OpenWeather;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IWeatherForecastServices
    {
        Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto);
        Task<AccuLocationWeatherResultDto> AccuWeatherResultWebClient(AccuLocationWeatherResultDto dto);
        Task<OpenWeatherViewModel> OpenWeatherResult(string city);
    }
}
