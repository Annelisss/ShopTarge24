using System;

namespace ShopTARge24.Core.Domain
{
    public class KindergartenFile
    {
        public Guid Id { get; set; }

        public Guid KindergartenId { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;

        public long Size { get; set; }

        public DateTime CreatedAt { get; set; }

        public string OriginalFileName { get; set; } = string.Empty;
    }
}
