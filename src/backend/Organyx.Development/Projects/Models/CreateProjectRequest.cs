namespace Organyx.Development.Projects.Models;

public record CreateProjectRequest
{
    public required string Key { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public string? Description { get; init; }
}