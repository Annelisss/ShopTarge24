using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto.RealEstates;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.RealEstates;

namespace ShopTARge24.Controllers
{
    public class RealEstateController : Controller
    {
        //private readonly IRealEstateServices _realEstateServices;

        //public RealEstateController(IRealEstateServices realEstateServices)
        //{
        //    _realEstateServices = realEstateServices;
        //}

        //public async Task<IActionResult> Index()
        //{
        //    var entities = await _realEstateServices.GetAll();

        //    var model = entities
        //        .Select(x => new RealEstateIndexViewModel
        //        {
        //            Id = x.Id,
        //            Area = x.Area,
        //            Location = x.Location,
        //            RoomNumber = x.RoomNumber,
        //            BuildingType = x.BuildingType
        //        })
        //        .ToList();

        //    return View(model);
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(RealEstateIndexViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    await _realEstateServices.Create(dto);
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var entity = await _realEstateServices.GetAsync(id);
        //    if (entity == null) return NotFound();

        //    var vm = new RealEstateIndexViewModel
        //    {
        //        Id = entity.Id,
        //        Area = entity.Area,
        //        Location = entity.Location,
        //        RoomNumber = entity.RoomNumber,
        //        BuildingType = entity.BuildingType
        //    };

        //    return View(vm);
        //}

        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var entity = await _realEstateServices.GetAsync(id);
        //    if (entity == null) return NotFound();

        //    var vm = new RealEstateIndexViewModel
        //    {
        //        Id = entity.Id,
        //        Area = entity.Area,
        //        Location = entity.Location,
        //        RoomNumber = entity.RoomNumber,
        //        BuildingType = entity.BuildingType
        //    };

        //    return View(vm);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, RealEstateIndexViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var dto = new KindergartenDto
        //    {
        //        Area = model.Area,
        //        Location = model.Location,
        //        RoomNumber = model.RoomNumber,
        //        BuildingType = model.BuildingType
        //    };

        //    await _realEstateServices.Update(id, dto);
        //    return RedirectToAction(nameof(Index));
        //}

        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var entity = await _realEstateServices.GetAsync(id);
        //    if (entity == null) return NotFound();

        //    var vm = new RealEstateIndexViewModel
        //    {
        //        Id = entity.Id,
        //        Area = entity.Area,
        //        Location = entity.Location,
        //        RoomNumber = entity.RoomNumber,
        //        BuildingType = entity.BuildingType
        //    };

        //    return View(vm);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    await _realEstateServices.Delete(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
