using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto.RealEstates;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARge24Context _context;

        public RealEstateServices(ShopTARge24Context context) => _context = context;

        public async Task<IEnumerable<RealEstate>> GetAll() =>
            await _context.RealEstates.AsNoTracking().ToListAsync();

        public async Task<RealEstate?> GetAsync(Guid id) =>
            await _context.RealEstates.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            var entity = new RealEstate
            {
                Id = Guid.NewGuid(),
                Area = dto.Area,
                Location = dto.Location,
                RoomNumber = dto.RoomNumber,
                BuildingType = dto.BuildingType,
            };

            _context.RealEstates.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RealEstate?> Update(Guid id, RealEstateDto dto)
        {
            var entity = await _context.RealEstates.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return null;

            entity.Area = dto.Area;
            entity.Location = dto.Location;
            entity.RoomNumber = dto.RoomNumber;
            entity.BuildingType = dto.BuildingType;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.RealEstates.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;

            _context.RealEstates.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
