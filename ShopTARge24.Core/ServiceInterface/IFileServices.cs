using Microsoft.AspNetCore.Http;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(SpaceshipDto dto, Spaceships domain);

        Task<FileToApi> RemoveImageFromApi(FileToApiDto dto);

        Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos);
        Task<List<FileToApi>> GetFiles(Guid entityId);
        Task RemoveFile(Guid fileId);

        Task SaveKindergartenFiles(Guid kindergartenId, List<IFormFile> files);

    }
}
