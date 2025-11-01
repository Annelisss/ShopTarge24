using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto.CocktailDtos;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.Cocktail;

namespace ShopTARge24.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailServices _cocktailServices;

        public CocktailsController(ICocktailServices cocktailServices)
        {
            _cocktailServices = cocktailServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCocktails(SearchCocktailViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Cocktail", "Cocktails", new { cocktail = model.SearchCocktail });
            }

            // kui mudel ei olnud ok, jää otsingu lehele
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Cocktail(string cocktail)
        {
            // 1) teeme DTO
            CocktailResultDto dto = new()
            {
                StrDrink = cocktail
            };

            // 2) kutsume service
            var cocktailResult = await _cocktailServices.GetCocktails(dto);

            // 3) KONTROLL – kui ei leitud
            if (cocktailResult == null || cocktailResult.Drinks == null || !cocktailResult.Drinks.Any())
            {
                ViewBag.Message = $"Kokteili „{cocktail}” ei leitud.";
                return View("Index");
            }

            // 4) kui leiti, võtame esimese joogi
            var drink = cocktailResult.Drinks[0];

            // 5) täidame viewmodeli
            CocktailViewModel vm = new()
            {
                IdDrink = drink.IdDrink,
                StrDrink = drink.StrDrink,
                StrDrinkAlternate = drink.StrDrinkAlternate,
                StrTags = drink.StrTags,
                StrVideo = drink.StrVideo,
                StrCategory = drink.StrCategory,
                StrIBA = drink.StrIBA,
                StrAlcoholic = drink.StrAlcoholic,
                StrGlass = drink.StrGlass,
                StrInstructions = drink.StrInstructions,
                StrInstructionsES = drink.StrInstructionsES,
                StrInstructionsDE = drink.StrInstructionsDE,
                StrInstructionsFR = drink.StrInstructionsFR,
                StrInstructionsIT = drink.StrInstructionsIT,
                StrInstructionsZHHANS = drink.StrInstructionsZHHANS,
                StrInstructionsZHHANT = drink.StrInstructionsZHHANT,
                StrDrinkThumb = drink.StrDrinkThumb,
                StrIngredient1 = drink.StrIngredient1,
                StrIngredient2 = drink.StrIngredient2,
                StrIngredient3 = drink.StrIngredient3,
                StrIngredient4 = drink.StrIngredient4,
                StrIngredient5 = drink.StrIngredient5,
                StrIngredient6 = drink.StrIngredient6,
                StrIngredient7 = drink.StrIngredient7,
                StrIngredient8 = drink.StrIngredient8,
                StrIngredient9 = drink.StrIngredient9,
                StrIngredient10 = drink.StrIngredient10,
                StrIngredient11 = drink.StrIngredient11,
                StrIngredient12 = drink.StrIngredient12,
                StrIngredient13 = drink.StrIngredient13,
                StrIngredient14 = drink.StrIngredient14,
                StrIngredient15 = drink.StrIngredient15,
                StrMeasure1 = drink.StrMeasure1,
                StrMeasure2 = drink.StrMeasure2,
                StrMeasure3 = drink.StrMeasure3,
                StrMeasure4 = drink.StrMeasure4,
                StrMeasure5 = drink.StrMeasure5,
                StrMeasure6 = drink.StrMeasure6,
                StrMeasure7 = drink.StrMeasure7,
                StrMeasure8 = drink.StrMeasure8,
                StrMeasure9 = drink.StrMeasure9,
                StrMeasure10 = drink.StrMeasure10,
                StrMeasure11 = drink.StrMeasure11,
                StrMeasure12 = drink.StrMeasure12,
                StrMeasure13 = drink.StrMeasure13,
                StrMeasure14 = drink.StrMeasure14,
                StrMeasure15 = drink.StrMeasure15,
                StrImageSource = drink.StrImageSource,
                StrImageAttribution = drink.StrImageAttribution,
                StrCreativeCommonsConfirmed = drink.StrCreativeCommonsConfirmed,
                DateModified = drink.DateModified
            };

            // 6) tagastame vaate
            return View(vm);
        }
    }
}
