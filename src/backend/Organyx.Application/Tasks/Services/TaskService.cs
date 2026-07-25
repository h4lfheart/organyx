using Organyx.Application.Projects.Repositories;
using Organyx.Application.Statuses.Repositories;
using Organyx.Application.Tasks.Models;
using Organyx.Application.Tasks.Repositories;
using Organyx.Infrastructure.Errors;

namespace Organyx.Application.Tasks.Services;

public interface ITaskService
{
    Task<TasksResponse> GetTasksAsync(string projectSlug);
}

public class TaskService(
    ITaskRepository taskRepository,
    IStatusRepository statusRepository,
    IProjectRepository projectRepository
) : ITaskService
{
    public async Task<TasksResponse> GetTasksAsync(string projectSlug)
    {
        var project = await projectRepository.GetBySlugAsync(projectSlug)
                      ?? throw new NotFoundException("Project not found.");

        var tasksTask = taskRepository.GetByProjectIdAsync(project.Id);
        var statusesTask = statusRepository.GetByProjectIdAsync(project.Id);
        await Task.WhenAll(tasksTask, statusesTask);

        var tasks = await tasksTask;
        var statuses = await statusesTask;
        var statusById = statuses.ToDictionary(status => status.Id);

        return new TasksResponse
        {
            Entries = tasks.Select(task =>
            {
                if (!statusById.TryGetValue(task.StatusId, out var status))
                    throw new BusinessRuleException($"Task {project.Key}-{task.Number} has no status.");

                return task.ToResponse(project.Key, status);
            })
        };
    }
}
