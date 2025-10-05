using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.Controllers
{
    public class RealEstateController : Controller
    {
        private readonly IRealEstateServices _service;

        public RealEstateController(IRealEstateServices service)
        {
            _service = service;
        }

        // GET: /RealEstate
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        // GET: /RealEstate/Details/{id}
        public async Task<IActionResult> Details(Guid id)
        {
            var item = await _service.GetAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // GET: /RealEstate/Create
        public IActionResult Create()
        {
            return View(new RealEstate());
        }

        // POST: /RealEstate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RealEstate model)
        {
            if (!ModelState.IsValid) return View(model);
            var created = await _service.CreateAsync(model);
            return RedirectToAction(nameof(Details), new { id = created.Id });
        }

        // GET: /RealEstate/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var item = await _service.GetAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /RealEstate/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, RealEstate model)
        {
            if (!ModelState.IsValid) return View(model);

            var updated = await _service.UpdateAsync(id, model);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Details), new { id });
        }

        // GET: /RealEstate/Delete/{id}
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _service.GetAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: /RealEstate/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
