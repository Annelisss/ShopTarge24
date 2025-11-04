using Microsoft.AspNetCore.Mvc;
using ShopTARge24.Core.Dto.KindergartenDto;
using ShopTARge24.Core.ServiceInterface;
using System.IO;

namespace ShopTARge24.Controllers
{
    public class KindergartenController : Controller
    {
        private readonly IKindergartenService _service;
        private readonly IFileServices _fileService;

        public KindergartenController(
            IKindergartenService service,
            IFileServices fileService)
        {
            _service = service;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAll();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new KindergartenDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KindergartenDto dto, List<IFormFile> files)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var created = await _service.Create(dto);

            if (files != null && files.Count > 0)
            {
                await _fileService.SaveKindergartenFiles(created.Id, files);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var entity = await _service.GetAsync(id);
            if (entity == null)
                return NotFound();

            var files = await _fileService.GetFiles(id);
            ViewBag.Files = files;

            return View(entity);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var entity = await _service.GetAsync(id);
            if (entity == null)
                return NotFound();

            var dto = new KindergartenDto
            {
                Id = entity.Id,
                GroupName = entity.GroupName,
                ChildrenCount = entity.ChildrenCount,
                KindergartenName = entity.KindergartenName,
                TeacherName = entity.TeacherName,
                ImageName = entity.ImageName
            };

            var files = await _fileService.GetFiles(id);
            ViewBag.Files = files;

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, KindergartenDto dto, List<IFormFile> files)
        {
            if (id != dto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
          
                ViewBag.Files = await _fileService.GetFiles(id);
                return View(dto);
            }

            await _service.Update(dto);

       
            if (files != null && files.Count > 0)
            {
                await _fileService.SaveKindergartenFiles(id, files);
            }

            return RedirectToAction(nameof(Edit), new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFile(Guid fileId, Guid kindergartenId)
        {
            await _fileService.RemoveFile(fileId);
            return RedirectToAction(nameof(Edit), new { id = kindergartenId });
        }
    }
}
