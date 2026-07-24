namespace Organyx.Development.Statuses.Models;

public record StatusesResponse
{
    public required IEnumerable<StatusResponseEntry> Entries { get; init; }
}

public record StatusResponseEntry
{
    public required Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required string Name { get; init; }
    public required int Position { get; init; }
    public required bool IsDefault { get; init; }
}