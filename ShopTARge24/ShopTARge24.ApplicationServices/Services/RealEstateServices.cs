using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class RealEstateServices : IRealEstateServices
    {
        private readonly ShopTARge24Context _context;

        public RealEstateServices(ShopTARge24Context context)
        {
            _context = context;
        }

        public Task<List<RealEstate>> GetAllAsync()
            => _context.RealEstates
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

        public Task<RealEstate?> GetAsync(Guid id)
            => DetailAsync(id);

        public async Task<RealEstate> CreateAsync(RealEstate entity)
        {
            var dto = new RealEstateDto
            {
                Id = entity.Id,
                Area = entity.Area,
                Location = entity.Location,
                RoomNumber = entity.RoomNumber,
                BuildingType = entity.BuildingType
            };
            return await Create(dto);
        }

        public async Task<RealEstate?> UpdateAsync(Guid id, RealEstate entity)
        {
            var dto = new RealEstateDto
            {
                Id = id,
                Area = entity.Area,
                Location = entity.Location,
                RoomNumber = entity.RoomNumber,
                BuildingType = entity.BuildingType
            };
            return await Update(dto);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var deleted = await Delete(id);
            return deleted != null;
        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {
            var entity = new RealEstate
            {
                Id = dto.Id is { } g && g != Guid.Empty ? g : Guid.NewGuid(),
                Area = dto.Area,
                Location = dto.Location,
                RoomNumber = dto.RoomNumber,
                BuildingType = dto.BuildingType,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = null
            };

            await _context.RealEstates.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RealEstate?> Update(RealEstateDto dto)
        {
            if (dto.Id == null || dto.Id == Guid.Empty)
                throw new ArgumentException("Id is required for update");

            var entity = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (entity == null)
                return null;

            entity.Area = dto.Area;
            entity.Location = dto.Location;
            entity.RoomNumber = dto.RoomNumber;
            entity.BuildingType = dto.BuildingType;
            entity.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<RealEstate?> DetailAsync(Guid id)
            => _context.RealEstates.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<RealEstate?> Delete(Guid id)
        {
            var entity = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null) return null;

            _context.RealEstates.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
