using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.OpenWeather;

namespace ShopTARge24.Controllers
{
    public class OpenWeatherController : Controller
    {
        private readonly IOpenWeatherService _openWeatherService;

        public OpenWeatherController(IOpenWeatherService openWeatherService)
        {
            _openWeatherService = openWeatherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
 
            return View(new OpenWeatherViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(OpenWeatherViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _openWeatherService.GetCurrentWeather(model.City!);

            if (result == null)
            {
                ModelState.AddModelError(string.Empty, "City not found or API error.");
                return View(model);
            }

            model.CityName = result.Name;
            model.Description = result.Weather?.FirstOrDefault()?.Description;
            model.TempC = result.Main?.Temp;
            model.Humidity = result.Main?.Humidity;

            return View(model);
        }
    }
}
