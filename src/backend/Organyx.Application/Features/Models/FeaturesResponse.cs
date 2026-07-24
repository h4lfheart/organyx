namespace Organyx.Application.Features.Models;

public record FeaturesResponse
{
    public required IEnumerable<FeatureResponseEntry> Entries { get; init; }
}

public record FeatureResponseEntry
{
    public required string Id { get; init; }
    public required string ProjectId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
