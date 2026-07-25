using Organyx.Development.Tasks.Models;
using Organyx.Infrastructure.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Development.Tasks;

public static class TaskMappers
{
    extension(CreateTaskRequest request)
    {
        public ProjectTask ToTable(Guid projectId, Guid statusId) => new()
        {
            ProjectId = projectId,
            Title = request.Title,
            Description = request.Description,
            Priority = PriorityMapping.ToDatabase(request.Priority),
            FeatureId = request.FeatureId,
            StatusId = statusId
        };
    }

    extension(ProjectTask task)
    {
        public TaskResponseEntry ToResponse(string projectKey) => new()
        {
            Id = task.Id,
            ProjectId = task.ProjectId,
            Key = $"{projectKey}-{task.Number}",
            Number = task.Number,
            Title = task.Title,
            Description = task.Description,
            Priority = PriorityMapping.FromDatabase(task.Priority),
            FeatureId = task.FeatureId,
            StatusId = task.StatusId
        };
    }
}