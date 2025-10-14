using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Domain;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileService
    {

        Task<KindergartenFile> ToEntityAsync(IFormFile file, int kindergartenId);

        bool IsImage(IFormFile file);
    }
}
