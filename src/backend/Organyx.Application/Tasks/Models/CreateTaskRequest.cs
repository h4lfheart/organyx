namespace Organyx.Application.Tasks.Models;

public record CreateTaskRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public Priority Priority { get; init; } = Priority.Medium;
    public string? FeatureId { get; init; }
    public string? StatusId { get; init; }
}
