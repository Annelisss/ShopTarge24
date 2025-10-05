using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopTARge24.Core.Domain;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IRealEstateServices
    {
        Task<List<RealEstate>> GetAllAsync();
        Task<RealEstate?> GetAsync(Guid id);
        Task<RealEstate> CreateAsync(RealEstate entity);
        Task<RealEstate?> UpdateAsync(Guid id, RealEstate entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
