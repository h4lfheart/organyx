namespace Organyx.Development.Projects.Models;

public record ProjectsResponse
{
    public required IEnumerable<ProjectResponseEntry> Entries { get; init; }
}

public record ProjectResponseEntry
{
    public required Guid Id { get; init; }
    public required string Key { get; init; }
    public required string Slug { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
}
