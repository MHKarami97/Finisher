namespace Finisher.Infrastructure.Data.Seed;

using System.Text.Json.Serialization;

public class CityJsonDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("province_id")]
    public int? ProvinceId { get; set; }

    [JsonPropertyName("county_id")]
    public long? CountyId { get; set; }
}
