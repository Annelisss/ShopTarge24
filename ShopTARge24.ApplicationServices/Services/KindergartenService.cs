using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto.KindergartenDto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenService : IKindergartenService
    {
        private readonly ShopTARge24Context _context;

        public KindergartenService(ShopTARge24Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Kindergartens>> GetAll()
        {
            return await _context.Kindergartens
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Kindergartens> GetAsync(Guid id)
        {
            return await _context.Kindergartens
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<Kindergartens> Create(KindergartenDto dto)
        {
            var entity = new Kindergartens
            {
                Id = Guid.NewGuid(),
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                TeacherName = dto.TeacherName,
                ImageName = dto.ImageName,       
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Kindergartens.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Kindergartens> Update(KindergartenDto dto)
        {
            var entity = await _context.Kindergartens
                .FirstOrDefaultAsync(k => k.Id == dto.Id);

            if (entity == null)
                return null!;

            entity.GroupName = dto.GroupName;
            entity.ChildrenCount = dto.ChildrenCount;
            entity.KindergartenName = dto.KindergartenName;
            entity.TeacherName = dto.TeacherName;
            entity.ImageName = dto.ImageName;    
            entity.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Kindergartens.FirstOrDefaultAsync(k => k.Id == id);
            if (entity == null)
                return false;

            _context.Kindergartens.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
