using System.Text.Json.Serialization;

namespace ShopTARge24.Core.Dto.ChuckNorris
{
    public class ChuckNorrisResultDto
    {
        //public List<string> Categories { get; set; } = new List<string>();
        public string CreatedAt { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string UpdatedAt { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
