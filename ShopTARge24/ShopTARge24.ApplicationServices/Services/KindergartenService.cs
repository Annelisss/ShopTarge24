using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class KindergartenService : IKindergartenService
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileService _fileService;

        public KindergartenService(
            ShopTARge24Context context,
            IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<List<Kindergarten>> GetAllAsync()
        {
            return await _context.Kindergartens
                .Include(k => k.Files)
                .ToListAsync();
        }

        public async Task<Kindergarten?> GetAsync(int id)
        {
            return await _context.Kindergartens
                .Include(k => k.Files)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Kindergarten> CreateAsync(Kindergarten kindergarten)
        {
            _context.Kindergartens.Add(kindergarten);
            await _context.SaveChangesAsync();
            return kindergarten;
        }

        public async Task<Kindergarten?> UpdateAsync(Kindergarten kindergarten)
        {
            var existing = await _context.Kindergartens.FindAsync(kindergarten.Id);
            if (existing == null) return null;

            existing.GroupName = kindergarten.GroupName;
            existing.ChildrenCount = kindergarten.ChildrenCount;
            existing.KindergartenName = kindergarten.KindergartenName;
            existing.TeacherName = kindergarten.TeacherName;
            existing.CreatedAt = kindergarten.CreatedAt;
            existing.UpdatedAt = kindergarten.UpdatedAt;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Kindergartens.FindAsync(id);
            if (existing == null) return false;

            _context.Kindergartens.Remove(existing);
            await _context.SaveChangesAsync();
            // FK + cascade delete korral lähevad ka pildid
            return true;
        }

        public async Task AddFilesAsync(int kindergartenId, List<IFormFile> files)
        {
            foreach (var f in files.Where(f => f is { Length: > 0 }))
            {
                if (!_fileService.IsImage(f)) continue;

                var entity = await _fileService.ToEntityAsync(f, kindergartenId);
                _context.KindergartenFiles.Add(entity);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePhotoAsync(Guid photoId)
        {
            var photo = await _context.KindergartenFiles.FindAsync(photoId);
            if (photo == null) return false;

            _context.KindergartenFiles.Remove(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<KindergartenFile?> GetFileAsync(Guid id)
        {
            return await _context.KindergartenFiles
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
