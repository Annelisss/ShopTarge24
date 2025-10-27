using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.AccuWeathers;

namespace ShopTARge24.Controllers
{
    public class AccuWeathersController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;

        public AccuWeathersController
            (
                IWeatherForecastServices weatherForecastServices
            )
        {
            _weatherForecastServices = weatherForecastServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(AccuWeathersSearchViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("City", "AccuWeathers", new { city = model.CityName });
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult City(string city)
        {
            AccuLocationWeatherResultDto dto = new();
            dto.CityName = city;

            _weatherForecastServices.AccuWeatherResult(dto);


            return View(dto);
        }
    }
}
