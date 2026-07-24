namespace Organyx.Application.Statuses.Models;

public record StatusesResponse
{
    public required IEnumerable<StatusResponseEntry> Entries { get; init; }
}

public record StatusResponseEntry
{
    public required string Id { get; init; }
    public required string ProjectId { get; init; }
    public required string Name { get; init; }
    public required int Position { get; init; }
}
