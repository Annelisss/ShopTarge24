using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.Controllers
{
    public class KindergartensController : Controller
    {
        private readonly IKindergartenService _kgService;

        public KindergartensController(IKindergartenService kgService)
        {
            _kgService = kgService;
        }

        public async Task<IActionResult> Index()
        {

            var list = await _kgService.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kindergarten = await _kgService.GetAsync(id.Value);
            if (kindergarten == null) return NotFound();

            return View(kindergarten);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,ChildrenCount,KindergartenName,TeacherName,CreatedAt,UpdatedAt")] Kindergarten kindergarten)
        {
            if (!ModelState.IsValid) return View(kindergarten);

            var created = await _kgService.CreateAsync(kindergarten);

            return RedirectToAction(nameof(Edit), new { id = created.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var kindergarten = await _kgService.GetAsync(id.Value);
            if (kindergarten == null) return NotFound();

            return View(kindergarten);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,ChildrenCount,KindergartenName,TeacherName,CreatedAt,UpdatedAt")] Kindergarten kindergarten)
        {
            if (id != kindergarten.Id) return NotFound();
            if (!ModelState.IsValid) return View(kindergarten);

            var updated = await _kgService.UpdateAsync(kindergarten);
            if (updated == null) return NotFound();

            return RedirectToAction(nameof(Edit), new { id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kindergarten = await _kgService.GetAsync(id.Value);
            if (kindergarten == null) return NotFound();

            return View(kindergarten);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _kgService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Photo(Guid id)
        {
            var file = await _kgService.GetFileAsync(id);
            if (file == null) return NotFound();

            return File(file.Data, file.ContentType, enableRangeProcessing: true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int id, List<IFormFile> files)
        {
            await _kgService.AddFilesAsync(id, files);
            return RedirectToAction(nameof(Edit), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhoto(Guid id, int kindergartenId, string? returnTo = null)
        {
            await _kgService.DeletePhotoAsync(id);

            if (string.Equals(returnTo, "Delete", StringComparison.OrdinalIgnoreCase))
                return RedirectToAction(nameof(Delete), new { id = kindergartenId });

            return RedirectToAction(nameof(Edit), new { id = kindergartenId });
        }
    }
}
