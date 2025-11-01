using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.ServiceInterface;

namespace ShopTARge24.ApplicationServices.Services
{
    public class FileService : IFileService
    {
        private static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp",
            "image/bmp"
        };

        public bool IsImage(IFormFile file)
            => file != null && file.Length > 0 && AllowedContentTypes.Contains(file.ContentType ?? "");

        public async Task<KindergartenFile> ToEntityAsync(IFormFile file, int kindergartenId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Tühi fail või faili ei valitud.");

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            return new KindergartenFile
            {
                Id = Guid.NewGuid(),
                KindergartenId = kindergartenId,
                FileName = Path.GetFileName(file.FileName),
                ContentType = file.ContentType ?? "application/octet-stream",
                Size = file.Length,
                Data = ms.ToArray()
            };
        }
    }
}
