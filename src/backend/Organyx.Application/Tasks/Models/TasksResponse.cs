namespace Organyx.Application.Tasks.Models;

public record TasksResponse
{
    public required IEnumerable<TaskResponseEntry> Entries { get; init; }
}

public record TaskResponseEntry
{
    public required string Id { get; init; }
    public required string ProjectId { get; init; }
    public required string Key { get; init; }
    public required int Number { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required Priority Priority { get; init; }
    public string? FeatureId { get; init; }
    public string? StatusId { get; init; }
}
