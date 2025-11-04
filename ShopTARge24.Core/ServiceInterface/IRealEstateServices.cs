using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto.RealEstates;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<IEnumerable<RealEstate>> GetAll();
        Task<RealEstate?> GetAsync(Guid id);

        Task<RealEstate> Create(RealEstateDto dto);
        Task<RealEstate?> Update(Guid id, RealEstateDto dto);

        Task<bool> Delete(Guid id);
    }
}
