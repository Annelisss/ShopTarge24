using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Domain;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IKindergartenService
    {
        Task<List<Kindergarten>> GetAllAsync();
        Task<Kindergarten?> GetAsync(int id);
        Task<Kindergarten> CreateAsync(Kindergarten kindergarten);
        Task<Kindergarten?> UpdateAsync(Kindergarten kindergarten);
        Task<bool> DeleteAsync(int id);

        Task<KindergartenFile?> GetFileAsync(Guid id);

        Task AddFilesAsync(int kindergartenId, List<IFormFile> files);
        Task<bool> DeletePhotoAsync(Guid photoId);
    }
}
