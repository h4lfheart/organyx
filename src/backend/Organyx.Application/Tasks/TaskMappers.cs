using Organyx.Application.Tasks.Models;
using Organyx.Infrastructure.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Application.Tasks;

public static class TaskMappers
{
    extension(ProjectTask task)
    {
        public TaskResponseEntry ToResponse(string projectKey, Status status, string? featureSlug) => new()
        {
            Id = task.Id,
            Key = $"{projectKey}-{task.Number}",
            Title = task.Title,
            Description = task.Description,
            FeatureSlug = featureSlug,
            Status = status.ToBadge(),
            Priority = PriorityMapping.FromDatabase(task.Priority),
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
    }

    extension(Status status)
    {
        public TaskStatusBadge ToBadge() => new()
        {
            Id = status.Id,
            Name = status.Name
        };
    }
}
