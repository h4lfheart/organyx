using System.Net;
using Organyx.Application.Features.Repositories;
using Organyx.Application.Projects.Repositories;
using Organyx.Application.Statuses.Repositories;
using Organyx.Application.Tasks.Models;
using Organyx.Application.Tasks.Repositories;

namespace Organyx.Application.Tasks.Services;

public interface ITaskService
{
    Task<TasksResponse?> GetTasksAsync(
        string projectId,
        string? featureId,
        string? statusId,
        Priority? priority,
        string? search);

    Task<TaskResponseEntry?> GetTaskAsync(string projectId, string taskId);

    Task<(HttpStatusCode Status, TaskResponseEntry? Task)> CreateAsync(string projectId, CreateTaskRequest request);

    Task<(HttpStatusCode Status, TaskResponseEntry? Task)> UpdateAsync(
        string projectId,
        string taskId,
        UpdateTaskRequest request);

    Task<HttpStatusCode> DeleteAsync(string projectId, string taskId);
}

public class TaskService(
    ITaskRepository taskRepository,
    IProjectRepository projectRepository,
    IFeatureRepository featureRepository,
    IStatusRepository statusRepository
) : ITaskService
{
    public async Task<TasksResponse?> GetTasksAsync(
        string projectId,
        string? featureId,
        string? statusId,
        Priority? priority,
        string? search)
    {
        var project = await projectRepository.GetByIdAsync(projectId);
        if (project is null)
            return null;

        var priorityValue = priority is null ? null : PriorityMapping.ToDatabase(priority.Value);
        var tasks = await taskRepository.GetByProjectIdAsync(projectId, featureId, statusId, priorityValue, search);

        return new TasksResponse
        {
            Entries = tasks.Select(task => task.ToResponse(project.Key))
        };
    }

    public async Task<TaskResponseEntry?> GetTaskAsync(string projectId, string taskId)
    {
        var project = await projectRepository.GetByIdAsync(projectId);
        if (project is null)
            return null;

        var task = await taskRepository.GetByIdAsync(taskId);
        if (task is null || task.ProjectId != projectId)
            return null;

        return task.ToResponse(project.Key);
    }

    public async Task<(HttpStatusCode Status, TaskResponseEntry? Task)> CreateAsync(string projectId, CreateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return (HttpStatusCode.BadRequest, null);

        var project = await projectRepository.GetByIdAsync(projectId);
        if (project is null)
            return (HttpStatusCode.NotFound, null);

        var validation = await ValidateReferencesAsync(projectId, request.FeatureId, request.StatusId);
        if (validation != HttpStatusCode.OK)
            return (validation, null);

        var created = await taskRepository.InsertAsync(request.ToTable(projectId));
        return created is not null
            ? (HttpStatusCode.Created, created.ToResponse(project.Key))
            : (HttpStatusCode.InternalServerError, null);
    }

    public async Task<(HttpStatusCode Status, TaskResponseEntry? Task)> UpdateAsync(
        string projectId,
        string taskId,
        UpdateTaskRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return (HttpStatusCode.BadRequest, null);

        var project = await projectRepository.GetByIdAsync(projectId);
        if (project is null)
            return (HttpStatusCode.NotFound, null);

        var existing = await taskRepository.GetByIdAsync(taskId);
        if (existing is null || existing.ProjectId != projectId)
            return (HttpStatusCode.NotFound, null);

        var validation = await ValidateReferencesAsync(projectId, request.FeatureId, request.StatusId);
        if (validation != HttpStatusCode.OK)
            return (validation, null);

        var updated = await taskRepository.UpdateAsync(
            taskId,
            request.Title,
            request.Description,
            PriorityMapping.ToDatabase(request.Priority),
            request.FeatureId,
            request.StatusId);

        return updated is not null
            ? (HttpStatusCode.OK, updated.ToResponse(project.Key))
            : (HttpStatusCode.NotFound, null);
    }

    public async Task<HttpStatusCode> DeleteAsync(string projectId, string taskId)
    {
        var existing = await taskRepository.GetByIdAsync(taskId);
        if (existing is null || existing.ProjectId != projectId)
            return HttpStatusCode.NotFound;

        await taskRepository.DeleteAsync(taskId);
        return HttpStatusCode.NoContent;
    }

    private async Task<HttpStatusCode> ValidateReferencesAsync(string projectId, string? featureId, string? statusId)
    {
        if (!string.IsNullOrWhiteSpace(featureId))
        {
            var feature = await featureRepository.GetByIdAsync(featureId);
            if (feature is null || feature.ProjectId != projectId)
                return HttpStatusCode.BadRequest;
        }

        if (!string.IsNullOrWhiteSpace(statusId))
        {
            var status = await statusRepository.GetByIdAsync(statusId);
            if (status is null || status.ProjectId != projectId)
                return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }
}
