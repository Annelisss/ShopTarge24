using System;

namespace ShopTARge24.Core.Dto
{
    public class FileToApiDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string Extension { get; set; } = string.Empty;

        public byte[] FileBytes { get; set; } = Array.Empty<byte>();

        public string FileType { get; set; } = string.Empty;

        public string? ExistingFilePath { get; set; }

        public Guid? SpaceshipId { get; set; }

        public Guid? RealEstateId { get; set; }
    }
}
