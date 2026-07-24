namespace Organyx.Development.Projects.Models;

public record UpdateProjectRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }
}