namespace Organyx.Application.Projects.Models;

public record ProjectsResponse
{
    public required IEnumerable<ProjectResponseEntry> Entries { get; init; }
}

public record ProjectResponseEntry
{
    public required string Id { get; init; }
    public required string Key { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
}
