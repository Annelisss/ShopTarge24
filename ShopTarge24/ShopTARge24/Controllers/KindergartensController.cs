using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.Controllers
{
    public class KindergartensController : Controller
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileService _fileService;

        public KindergartensController(ShopTARge24Context context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {

            var list = await _context.Kindergartens
                .Include(k => k.Files)         
                .ToListAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var kindergarten = await _context.Kindergartens
                .Include(k => k.Files)   
                .FirstOrDefaultAsync(m => m.Id == id);

            if (kindergarten == null) return NotFound();

            return View(kindergarten);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupName,ChildrenCount,KindergartenName,TeacherName,CreatedAt,UpdatedAt")] Kindergarten kindergarten)
        {
            if (!ModelState.IsValid) return View(kindergarten);

            _context.Add(kindergarten);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id = kindergarten.Id });
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var kindergarten = await _context.Kindergartens
                .Include(k => k.Files) 
                .FirstOrDefaultAsync(x => x.Id == id);

            if (kindergarten == null) return NotFound();

            return View(kindergarten);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupName,ChildrenCount,KindergartenName,TeacherName,CreatedAt,UpdatedAt")] Kindergarten kindergarten)
        {
            if (id != kindergarten.Id) return NotFound();
            if (!ModelState.IsValid) return View(kindergarten);

            try
            {
                _context.Update(kindergarten);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KindergartenExists(kindergarten.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Edit), new { id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var kindergarten = await _context.Kindergartens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kindergarten == null) return NotFound();

            return View(kindergarten);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kindergarten = await _context.Kindergartens.FindAsync(id);
            if (kindergarten != null)
            {
                _context.Kindergartens.Remove(kindergarten);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KindergartenExists(int id)
            => _context.Kindergartens.Any(e => e.Id == id);

        [HttpGet]
        public async Task<IActionResult> Photo(Guid id)
        {
            var photo = await _context.KindergartenFiles.FirstOrDefaultAsync(x => x.Id == id);
            if (photo == null) return NotFound();
            return File(photo.Data, photo.ContentType, enableRangeProcessing: true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(int id, List<IFormFile> files)
        {
            var kg = await _context.Kindergartens.FindAsync(id);
            if (kg == null) return NotFound();

            foreach (var f in files.Where(f => f is { Length: > 0 }))
            {
                if (!_fileService.IsImage(f)) continue;
                var entity = await _fileService.ToEntityAsync(f, id);
                _context.KindergartenFiles.Add(entity);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Edit), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhoto(Guid id, int kindergartenId)
        {
            var photo = await _context.KindergartenFiles.FindAsync(id);
            if (photo == null) return NotFound();

            _context.KindergartenFiles.Remove(photo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = kindergartenId });
        }
    }
}
