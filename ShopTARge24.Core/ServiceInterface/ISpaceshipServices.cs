using ShopTARge24.Core.Dto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<SpaceshipDto> Create(SpaceshipDto dto);
        Task<SpaceshipDto?> GetAsync(Guid id);
        Task<IEnumerable<SpaceshipDto>> GetAll();
        Task<SpaceshipDto> Update(Guid id, SpaceshipDto dto);
        Task<bool> Delete(Guid id);
    }
}
