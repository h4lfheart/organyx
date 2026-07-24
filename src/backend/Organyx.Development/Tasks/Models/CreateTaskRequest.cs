namespace Organyx.Development.Tasks.Models;

public record CreateTaskRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public Priority Priority { get; init; } = Priority.Medium;
    public Guid? FeatureId { get; init; }
    public Guid? StatusId { get; init; }
}