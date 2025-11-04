namespace ShopTARge24.Core.Dto.KindergartenDto
{
    public class KindergartenDto
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string? ImageName { get; set; }

        public int ChildrenCount { get; set; }
        public string KindergartenName { get; set; }
        public string TeacherName { get; set; }
    }
}