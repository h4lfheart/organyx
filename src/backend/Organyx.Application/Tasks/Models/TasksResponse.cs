using Organyx.Infrastructure.Models;

namespace Organyx.Application.Tasks.Models;

public record TasksResponse
{
    public required IEnumerable<TaskResponseEntry> Entries { get; init; }
}

public record TaskResponseEntry
{
    public required Guid Id { get; init; }
    public required string Key { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public string? FeatureSlug { get; init; }
    public required TaskStatusBadge Status { get; init; }
    public required Priority Priority { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
    public required DateTimeOffset UpdatedAt { get; init; }
}

public record TaskStatusBadge
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required bool IsComplete { get; init; }
}
