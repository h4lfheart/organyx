using Organyx.Development.Features.Repositories;
using Organyx.Development.Projects.Repositories;
using Organyx.Development.Statuses.Repositories;
using Organyx.Development.Tasks.Models;
using Organyx.Development.Tasks.Repositories;
using Organyx.Infrastructure.Errors;

namespace Organyx.Development.Tasks.Services;

public interface ITaskService
{
    Task<TasksResponse> GetTasksAsync(
        Guid projectId,
        Guid? featureId,
        Guid? statusId,
        Priority? priority,
        string? search);

    Task<TaskResponseEntry> GetTaskAsync(Guid taskId);

    Task<Guid> CreateAsync(Guid projectId, CreateTaskRequest request);

    Task UpdateAsync(Guid taskId, UpdateTaskRequest request);

    Task DeleteAsync(Guid taskId);
}

public class TaskService(
    ITaskRepository taskRepository,
    IProjectRepository projectRepository,
    IFeatureRepository featureRepository,
    IStatusRepository statusRepository
) : ITaskService
{
    public async Task<TasksResponse> GetTasksAsync(
        Guid projectId,
        Guid? featureId,
        Guid? statusId,
        Priority? priority,
        string? search)
    {
        var project = await projectRepository.GetByIdAsync(projectId)
                      ?? throw new NotFoundException("Project not found.");

        var priorityValue = priority is null ? null : PriorityMapping.ToDatabase(priority.Value);
        var tasks = await taskRepository.GetByProjectIdAsync(projectId, featureId, statusId, priorityValue, search);

        return new TasksResponse
        {
            Entries = tasks.Select(task => task.ToResponse(project.Key))
        };
    }

    public async Task<TaskResponseEntry> GetTaskAsync(Guid taskId)
    {
        var task = await taskRepository.GetByIdAsync(taskId)
                   ?? throw new NotFoundException("Task not found.");

        var project = await projectRepository.GetByIdAsync(task.ProjectId)
                      ?? throw new NotFoundException("Project not found.");

        return task.ToResponse(project.Key);
    }

    public async Task<Guid> CreateAsync(Guid projectId, CreateTaskRequest request)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        var statusId = request.StatusId;
        if (statusId is null)
        {
            var defaultStatus = await statusRepository.GetDefaultByProjectIdAsync(projectId)
                                ?? throw new BusinessRuleException("Project has no default status.");
            statusId = defaultStatus.Id;
        }

        await ValidateReferencesAsync(projectId, request.FeatureId, statusId);

        var created = await taskRepository.InsertAsync(request.ToTable(projectId, statusId.Value))
                      ?? throw new InvalidOperationException("Failed to create task.");
        return created.Id;
    }

    public async Task UpdateAsync(Guid taskId, UpdateTaskRequest request)
    {
        var existing = await taskRepository.GetByIdAsync(taskId)
                       ?? throw new NotFoundException("Task not found.");

        await ValidateReferencesAsync(existing.ProjectId, request.FeatureId, request.StatusId);

        _ = await taskRepository.UpdateAsync(
            taskId,
            request.Title,
            request.Description,
            PriorityMapping.ToDatabase(request.Priority),
            request.FeatureId,
            request.StatusId) ?? throw new NotFoundException("Task not found.");
    }

    public async Task DeleteAsync(Guid taskId)
    {
        if (await taskRepository.GetByIdAsync(taskId) is null)
            throw new NotFoundException("Task not found.");

        await taskRepository.DeleteAsync(taskId);
    }

    private async Task ValidateReferencesAsync(Guid projectId, Guid? featureId, Guid? statusId)
    {
        if (featureId is not null)
        {
            var feature = await featureRepository.GetByIdAsync(featureId.Value);
            if (feature is null || feature.ProjectId != projectId)
                throw new BusinessRuleException("Feature does not belong to this project.");
        }

        if (statusId is not null)
        {
            var status = await statusRepository.GetByIdAsync(statusId.Value);
            if (status is null || status.ProjectId != projectId)
                throw new BusinessRuleException("Status does not belong to this project.");
        }
    }
}