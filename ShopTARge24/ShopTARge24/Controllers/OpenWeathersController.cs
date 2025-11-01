using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.ServiceInterface;
using System.Threading.Tasks;

namespace ShopTARge24.Controllers
{
    public class OpenWeathersController : Controller
    {
        private readonly IWeatherForecastServices _weatherService;

        public OpenWeathersController(IWeatherForecastServices weatherService)
        {
            _weatherService = weatherService;
        }

        // GET: /OpenWeathers/City?city=Tallinn
        public async Task<IActionResult> City(string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                city = "Tallinn";

            var model = await _weatherService.OpenWeatherResult(city);

            return View(model);   // otsib Views/OpenWeathers/City.cshtml
        }
    }
}
