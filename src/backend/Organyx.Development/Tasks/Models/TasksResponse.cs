using Organyx.Infrastructure.Models;

namespace Organyx.Development.Tasks.Models;

public record TasksResponse
{
    public required IEnumerable<TaskResponseEntry> Entries { get; init; }
}

public record TaskResponseEntry
{
    public required Guid Id { get; init; }
    public required Guid ProjectId { get; init; }
    public required string Key { get; init; }
    public required int Number { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required Priority Priority { get; init; }
    public Guid? FeatureId { get; init; }
    public Guid? StatusId { get; init; }
}