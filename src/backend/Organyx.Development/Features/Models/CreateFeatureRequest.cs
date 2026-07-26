namespace Organyx.Development.Features.Models;

public record CreateFeatureRequest
{
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public string? Description { get; init; }
    public Guid? StatusId { get; init; }
}
