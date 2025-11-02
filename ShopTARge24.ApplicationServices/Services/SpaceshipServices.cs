using Microsoft.EntityFrameworkCore;
using ShopTARge24.Core.Domain;
using ShopTARge24.Core.Dto;
using ShopTARge24.Core.ServiceInterface;
using ShopTARge24.Data;

namespace ShopTARge24.ApplicationServices.Services
{
    public class SpaceshipServices : ISpaceshipServices
    {
        private readonly ShopTARge24Context _context;
        private readonly IFileServices _fileServices;

        public SpaceshipServices(
            ShopTARge24Context context,
            IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<SpaceshipDto> Create(SpaceshipDto dto)
        {
            var entity = new Spaceships
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Classification = dto.Classification,
                BuiltDate = dto.BuiltDate,
                Crew = dto.Crew,
                EnginePower = dto.EnginePower,
                CreatedAt = DateTime.Now
            };

            await _context.Spaceships.AddAsync(entity);
            await _context.SaveChangesAsync();

            if (dto.Files != null && dto.Files.Count > 0)
            {
                _fileServices.FilesToApi(dto, entity);
            }

            dto.Id = entity.Id;
            dto.CreatedAt = entity.CreatedAt;
            return dto;
        }

        public async Task<SpaceshipDto?> GetAsync(Guid id)
        {

            var entity = await _context.Spaceships
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

     
            var files = await _context.FileToApis
                .Where(f => f.SpaceshipId == id)
                .Select(f => new FileToApiDto
                {
                    Id = f.Id,
     
                    ExistingFilePath = f.ExistingFilePath
                })
                .ToListAsync();

            return new SpaceshipDto
            {
                Id = entity.Id,
                Name = entity.Name,
                Classification = entity.Classification,
                BuiltDate = entity.BuiltDate,
                Crew = entity.Crew,
                EnginePower = entity.EnginePower,
                CreatedAt = entity.CreatedAt,
                ModifiedAt = entity.ModifiedAt,
                FileToApiDtos = files
            };
        }


        public async Task<IEnumerable<SpaceshipDto>> GetAll()
        {
            return await _context.Spaceships
                .Select(x => new SpaceshipDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Classification = x.Classification,
                    BuiltDate = x.BuiltDate,
                    Crew = x.Crew,
                    EnginePower = x.EnginePower,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt
                })
                .ToListAsync();
        }

        public async Task<SpaceshipDto> Update(Guid id, SpaceshipDto dto)
        {
            var entity = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new Exception("Spaceship not found");
            }

            entity.Name = dto.Name;
            entity.Classification = dto.Classification;
            entity.BuiltDate = dto.BuiltDate;
            entity.Crew = dto.Crew;
            entity.EnginePower = dto.EnginePower;
            entity.ModifiedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            if (dto.Files != null && dto.Files.Any())
            {
                _fileServices.FilesToApi(dto, entity);
            }

            dto.Id = entity.Id;
            dto.ModifiedAt = entity.ModifiedAt;
            return dto;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Spaceships
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            var apiFiles = await _context.FileToApis
                .Where(x => x.SpaceshipId == id)
                .ToArrayAsync();

            if (apiFiles.Length > 0)
            {
                var dtos = apiFiles.Select(f => new FileToApiDto
                {
                    Id = f.Id,
                    ExistingFilePath = f.ExistingFilePath
                }).ToArray();

                await _fileServices.RemoveImagesFromApi(dtos);
            }

            _context.Spaceships.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
