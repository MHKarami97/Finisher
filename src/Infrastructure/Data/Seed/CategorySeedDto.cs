using System.Text.Json.Serialization;

namespace Finisher.Infrastructure.Data.Seed;

public sealed class CategorySeedDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;

    [JsonPropertyName("children")]
    public List<CategorySeedDto>? Children { get; set; }
}
