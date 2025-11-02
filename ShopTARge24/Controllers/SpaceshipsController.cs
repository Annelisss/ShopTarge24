using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Models.Spaceships;

namespace ShopTARge24.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ISpaceshipServices _spaceshipServices;

        public SpaceshipsController(ISpaceshipServices spaceshipServices)
        {
            _spaceshipServices = spaceshipServices;
        }

        public async Task<IActionResult> Index()
        {
            var ships = await _spaceshipServices.GetAll();

            var model = ships.Select(s => new SpaceshipIndexViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Classification = s.Classification,
                BuiltDate = s.BuiltDate,
                Crew = s.Crew
            }).ToList();

            return View(model);
        }


        public IActionResult Create()
        {

            return View("Create", new SpaceshipCreateUpdateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpaceshipCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }

            var dto = new SpaceshipDto
            {
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                Files = vm.Files
            };

            await _spaceshipServices.Create(dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var ship = await _spaceshipServices.GetAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var ship = await _spaceshipServices.GetAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipCreateUpdateViewModel
            {
                Id = ship.Id,
                Name = ship.Name,
                Classification = ship.Classification,
                BuiltDate = ship.BuiltDate,
                Crew = ship.Crew,
                EnginePower = ship.EnginePower,
                CreatedAt = ship.CreatedAt,
                ModifiedAt = ship.ModifiedAt,
                Image = ship.FileToApiDtos?
                    .Select(f => new ImageViewModel
                    {
                        Filepath = f.ExistingFilePath ?? f.FileName,
                        ImageId = f.Id
                    })
                    .ToList()
            };

            return View("Create", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SpaceshipCreateUpdateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", vm);
            }

            var dto = new SpaceshipDto
            {
                Id = vm.Id,
                Name = vm.Name,
                Classification = vm.Classification,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = DateTime.Now,
                Files = vm.Files
            };

            await _spaceshipServices.Update(id, dto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var ship = await _spaceshipServices.GetAsync(id);
            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _spaceshipServices.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
