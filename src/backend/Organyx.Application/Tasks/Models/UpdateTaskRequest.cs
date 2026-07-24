namespace Organyx.Application.Tasks.Models;

public record UpdateTaskRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required Priority Priority { get; init; }
    public string? FeatureId { get; init; }
    public string? StatusId { get; init; }
}
