using System;
using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Core.Domain
{

    public class KindergartenFile
    {
        [Key]
        public Guid Id { get; set; }

        public int KindergartenId { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = default!;

        [Required, MaxLength(127)]
        public string ContentType { get; set; } = "application/octet-stream";

        public long Size { get; set; }

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Kindergarten Kindergarten { get; set; } = default!;
    }
}
