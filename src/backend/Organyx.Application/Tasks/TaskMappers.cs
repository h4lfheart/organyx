using Organyx.Application.Tasks.Models;
using Organyx.Application.Tasks.Tables;

namespace Organyx.Application.Tasks;

public static class TaskMappers
{
    extension(CreateTaskRequest request)
    {
        public ProjectTask ToTable(string projectId) => new()
        {
            ProjectId = projectId,
            Title = request.Title,
            Description = request.Description,
            Priority = PriorityMapping.ToDatabase(request.Priority),
            FeatureId = request.FeatureId,
            StatusId = request.StatusId
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
