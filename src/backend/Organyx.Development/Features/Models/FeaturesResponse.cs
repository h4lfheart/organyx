namespace Organyx.Development.Features.Models;

public record FeaturesResponse
{
    public required IEnumerable<FeatureResponseEntry> Entries { get; init; }
}

public record FeatureResponseEntry
{
    public required Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required string Slug { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public Guid? StatusId { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
}
