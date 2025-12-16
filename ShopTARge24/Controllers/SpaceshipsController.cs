using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Controllers
{
    [Authorize]
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly ISpaceshipServices _spaceshipServices;
        private readonly IFileServices _fileServices;

        public SpaceshipsController(
            ShopTARge24Context context,
            ISpaceshipServices spaceshipServices,
            IFileServices fileServices)
        {
            _context = context;
            _spaceshipServices = spaceshipServices;
            _fileServices = fileServices;
        }

        // ===================== INDEX =====================
        public async Task<IActionResult> Index()
        {
            var ships = await _spaceshipServices.GetAll();

            var result = ships.Select(x => new SpaceshipIndexViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Classification = x.Classification,
                BuiltDate = x.BuiltDate,
                Crew = x.Crew
            });

            return View(result);
        }

        // ===================== CREATE =====================
        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateUpdate", new SpaceshipCreateUpdateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateUpdate", vm);
            }

            var dto = new SpaceshipDto
            {
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                Files = vm.Files,
                FileToApiDtos = vm.Image
                    .Select(x => new FileToApiDto
                    {
                        Id = x.ImageId,
                        ExistingFilePath = x.Filepath,
                        SpaceshipId = x.SpaceshipId
                    }).ToArray()
            };

            await _spaceshipServices.Create(dto);
            return RedirectToAction(nameof(Index));
        }

        // ===================== UPDATE =====================
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var ship = await _spaceshipServices.GetAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id,
                    SpaceshipId = id
                }).ToArrayAsync();

            var vm = new SpaceshipCreateUpdateViewModel
            {
                Id = ship.Id,
                Name = ship.Name,
                Classification = ship.Classification,
                BuiltDate = ship.BuiltDate,
                Crew = ship.Crew,
                EnginePower = ship.EnginePower,
                CreatedAt = ship.CreatedAt,
                ModifiedAt = ship.ModifiedAt
            };

            vm.Image.AddRange(images);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, SpaceshipCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
                return View("CreateUpdate", vm);

            var dto = new SpaceshipDto
            {
                Id = id,
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files,
                FileToApiDtos = vm.Image.Select(x => new FileToApiDto
                {
                    Id = x.ImageId,
                    ExistingFilePath = x.Filepath,
                    SpaceshipId = x.SpaceshipId
                }).ToArray()
            };

            await _spaceshipServices.Update(id, dto);
            return RedirectToAction(nameof(Index));
        }

        // ===================== DELETE =====================
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ship = await _spaceshipServices.GetAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new SpaceshipDeleteViewModel
            {
                Id = ship.Id,
                Name = ship.Name,
                Classification = ship.Classification,
                BuiltDate = ship.BuiltDate,
                Crew = ship.Crew,
                EnginePower = ship.EnginePower,
                CreatedAt = ship.CreatedAt,
                ModifiedAt = ship.ModifiedAt
            };

            vm.ImageViewModels.AddRange(images);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            await _spaceshipServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // ===================== DETAILS =====================
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var ship = await _spaceshipServices.GetAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            var images = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .Select(y => new ImageViewModel
                {
                    Filepath = y.ExistingFilePath,
                    ImageId = y.Id
                }).ToArrayAsync();

            var vm = new SpaceshipDetailsViewModel
            {
                Id = ship.Id,
                Name = ship.Name,
                Classification = ship.Classification,
                BuiltDate = ship.BuiltDate,
                Crew = ship.Crew,
                EnginePower = ship.EnginePower,
                CreatedAt = ship.CreatedAt,
                ModifiedAt = ship.ModifiedAt
            };

            vm.Images.AddRange(images);

            return View(vm);
        }

        // ===================== REMOVE IMAGE =====================
        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageViewModel vm)
        {
            var dto = new FileToApiDto
            {
                Id = vm.ImageId
            };

            await _fileServices.RemoveImageFromApi(dto);
            return RedirectToAction(nameof(Index));
        }
    }
}
