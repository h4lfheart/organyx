using Organyx.Application.Features.Repositories;
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
    IFeatureRepository featureRepository,
    IStatusRepository statusRepository,
    IProjectRepository projectRepository
) : ITaskService
{
    public async Task<TasksResponse> GetTasksAsync(string projectSlug)
    {
        var project = await projectRepository.GetBySlugAsync(projectSlug)
                      ?? throw new NotFoundException("Project not found.");

        var tasksTask = taskRepository.GetByProjectIdAsync(project.Id);
        var featuresTask = featureRepository.GetByProjectIdAsync(project.Id);
        var statusesTask = statusRepository.GetByProjectIdAsync(project.Id);
        await Task.WhenAll(tasksTask, featuresTask, statusesTask);

        var tasks = await tasksTask;
        var features = await featuresTask;
        var statuses = await statusesTask;
        var featureById = features.ToDictionary(feature => feature.Id);
        var statusById = statuses.ToDictionary(status => status.Id);

        return new TasksResponse
        {
            Entries = tasks.Select(task =>
            {
                if (!statusById.TryGetValue(task.StatusId, out var status))
                    throw new BusinessRuleException($"Task {project.Key}-{task.Number} has no status.");

                string? featureSlug = null;
                if (task.FeatureId is { } featureId)
                {
                    if (!featureById.TryGetValue(featureId, out var feature))
                        throw new BusinessRuleException(
                            $"Task {project.Key}-{task.Number} references an invalid feature.");

                    featureSlug = feature.Slug;
                }

                return task.ToResponse(project.Key, status, featureSlug);
            })
        };
    }
}
