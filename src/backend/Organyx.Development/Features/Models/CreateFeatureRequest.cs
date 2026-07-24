namespace Organyx.Development.Features.Models;

public record CreateFeatureRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}