namespace Organyx.Application.Features.Models;

public record UpdateFeatureRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}
