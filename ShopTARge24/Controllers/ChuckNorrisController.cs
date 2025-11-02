using Microsoft.AspNetCore.Mvc;
using ShopTARge24.ApplicationServices.Services;
using ShopTARge24.Core.Dto.ChuckNorris;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.ChuckNorris;

namespace ShopTARge24.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _chuckNorrisServices;

        public ChuckNorrisController
            (
                IChuckNorrisServices chuckNorrisServices
            )
        {
            _chuckNorrisServices = chuckNorrisServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchChuckNorrisJokes()
        {
            return RedirectToAction(nameof(Joke));
        }

        [HttpGet]
        public async Task<IActionResult>Joke()
        {
            var joke = await _chuckNorrisServices.ChuckNorrisResultHttpClient();

            ChuckNorrisViewModel vm = new();

            //vm.Categories = joke.Categories;
            vm.CreatedAt = joke.CreatedAt;
            vm.IconUrl = joke.IconUrl;
            vm.Id = joke.Id;
            vm.UpdatedAt = joke.UpdatedAt;
            vm.Url = joke.Url;
            vm.Value = joke.Value;

            return View(vm);
        }
    }
}
