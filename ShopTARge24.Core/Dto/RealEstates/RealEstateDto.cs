namespace ShopTARge24.Core.Dto.RealEstates
{
    public class RealEstateDto
    {
        public Guid Id { get; set; }          
        public int Area { get; set; }
        public string Location { get; set; } = string.Empty;
        public int RoomNumber { get; set; }
        public string BuildingType { get; set; } = string.Empty;
    }
}
