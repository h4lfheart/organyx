using Organyx.Infrastructure.Models;

namespace Organyx.Development.Tasks.Models;

public record UpdateTaskRequest
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required Priority Priority { get; init; }
    public Guid? FeatureId { get; init; }
    public required Guid StatusId { get; init; }
}