namespace Organyx.Development.Features.Models;

public record FeaturesResponse
{
    public required IEnumerable<FeatureResponseEntry> Entries { get; init; }
}

public record FeatureResponseEntry
{
    public required Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}