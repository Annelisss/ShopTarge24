using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.RealEstate;
using static System.Net.Mime.MediaTypeNames;


namespace ShopTARge24.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IRealEstateServices _realEstateServices;

        public RealEstateController
            (
                ShopTARge24Context context,
                IRealEstateServices realEstateServices
            )
        {
            _context = context;
            _realEstateServices = realEstateServices;
        }

        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Area = x.Area,
                    BuildingType = x.BuildingType,
                    RoomNumber = x.RoomNumber,
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel result = new();

            return View("CreateUpdate", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                BuildingType = vm.BuildingType,
                RoomNumber = vm.RoomNumber,
                Location = vm.Location,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image
                    .Select(x => new FileToDatabaseDto
                    {
                        Id = x.Id,
                        ImageData = x.ImageData,
                        ImageTitle = x.ImageTitle,
                        RealEstateId = x.RealEstateId
                    }).ToArray()
            };

            var result = await _realEstateServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            RealEstateImageViewModel[] images = await FileFromDatabase(id);


            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.BuildingType = realEstate.BuildingType;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.Location = realEstate.Location;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Area = vm.Area,
                BuildingType = vm.BuildingType,
                RoomNumber = vm.RoomNumber,
                Location = vm.Location,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
            };

            var result = await _realEstateServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            RealEstateImageViewModel[] images = await FileFromDatabase(id);

            var vm = new RealEstateDeleteViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.BuildingType = realEstate.BuildingType;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.Location = realEstate.Location;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.Image.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var realEstate = await _realEstateServices.Delete(id);

            if (realEstate == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            //kasutada service classi meetodit, et info k'tte saada
            var realEstate = await _realEstateServices.DetailAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            RealEstateImageViewModel[] images = await FileFromDatabase(id);

            var vm = new RealEstateDetailsViewModel();

            vm.Id = realEstate.Id;
            vm.Area = realEstate.Area;
            vm.BuildingType = realEstate.BuildingType;
            vm.RoomNumber = realEstate.RoomNumber;
            vm.Location = realEstate.Location;
            vm.CreatedAt = realEstate.CreatedAt;
            vm.ModifiedAt = realEstate.ModifiedAt;
            vm.Images.AddRange(images);

            return View(vm);
        }

        private async Task<RealEstateImageViewModel[]> FileFromDatabase(Guid id)
        {
            return await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new RealEstateImageViewModel
                {
                    Id = y.Id,
                    RealEstateId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();
        }
    }
}
