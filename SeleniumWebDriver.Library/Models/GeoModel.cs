using System.Text.Json.Serialization;

namespace SeleniumWebDriver.Business.Models
{
    public class GeoModel
    {
        [JsonPropertyName("lat")]
        public string? Lat { get; set; }

        [JsonPropertyName("lng")]
        public string? Lng { get; set; }
    }
}