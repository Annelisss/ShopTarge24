using ShopTARge24.Core.Dto.OpenWeather;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IOpenWeatherService
    {
        Task<OpenWeatherResponseDto?> GetCurrentWeather(string city);
    }
}
