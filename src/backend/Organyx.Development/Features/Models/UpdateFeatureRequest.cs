namespace Organyx.Development.Features.Models;

public record UpdateFeatureRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
    public Guid? StatusId { get; init; }
}
