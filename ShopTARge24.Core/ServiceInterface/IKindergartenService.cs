using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto.KindergartenDto;

namespace ShopTARge24.Core.ServiceInterface
{
    public interface IKindergartenService
    {
        Task<IEnumerable<Kindergartens>> GetAll();
        Task<Kindergartens> GetAsync(Guid id);
        Task<Kindergartens> Create(KindergartenDto dto);
        Task<Kindergartens> Update(KindergartenDto dto);
        Task<bool> Delete(Guid id);
    }
}
