using System;

namespace ShopTARge24.Core.Domain
{
    public class Kindergartens
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string GroupName { get; set; } = string.Empty;
        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public string? ImageName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
