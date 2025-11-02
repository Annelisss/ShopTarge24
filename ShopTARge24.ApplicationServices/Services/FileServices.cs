using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;   
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge24Context _context;

        public FileServices(
            IHostEnvironment webHost,
            ShopTARge24Context context)
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(SpaceshipDto dto, Spaceships domain)
        {
            if (dto.Files == null || dto.Files.Count == 0)
            {
                return;
            }

            var uploadRoot = Path.Combine(_webHost.ContentRootPath, "wwwroot", "multipleFileUpload");
            if (!Directory.Exists(uploadRoot))
            {
                Directory.CreateDirectory(uploadRoot);
            }

            foreach (var file in dto.Files)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
                var filePath = Path.Combine(uploadRoot, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var path = new FileToApi
                {
                    Id = Guid.NewGuid(),
                    ExistingFilePath = uniqueFileName,
                    SpaceshipId = domain.Id
                };

                _context.FileToApis.Add(path);
            }

            _context.SaveChanges();
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var image = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (image == null)
            {
                return null;
            }

            var filePath = Path.Combine(
                _webHost.ContentRootPath,
                "wwwroot",
                "multipleFileUpload",
                image.ExistingFilePath ?? string.Empty);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FileToApis.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            var removed = new List<FileToApi>();

            foreach (var dto in dtos)
            {
                var image = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);

                if (image == null)
                    continue;

                var filePath = Path.Combine(
                    _webHost.ContentRootPath,
                    "wwwroot",
                    "multipleFileUpload",
                    image.ExistingFilePath ?? string.Empty);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _context.FileToApis.Remove(image);
                removed.Add(image);
            }

            await _context.SaveChangesAsync();

            return removed;
        }
    }
}
