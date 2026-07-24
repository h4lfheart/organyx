using Organyx.Development.Projects.Repositories;
using Organyx.Development.Statuses.Models;
using Organyx.Development.Statuses.Repositories;
using Organyx.Infrastructure.Errors;

namespace Organyx.Development.Statuses.Services;

public interface IStatusService
{
    Task<StatusesResponse> GetStatusesAsync(Guid projectId);
    Task<StatusResponseEntry> GetStatusAsync(Guid statusId);
    Task<Guid> CreateAsync(Guid projectId, CreateStatusRequest request);
    Task UpdateAsync(Guid statusId, UpdateStatusRequest request);
    Task DeleteAsync(Guid statusId);
}

public class StatusService(
    IStatusRepository statusRepository,
    IProjectRepository projectRepository
) : IStatusService
{
    public async Task<StatusesResponse> GetStatusesAsync(Guid projectId)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        var statuses = await statusRepository.GetByProjectIdAsync(projectId);
        return new StatusesResponse
        {
            Entries = statuses.Select(status => status.ToResponse())
        };
    }

    public async Task<StatusResponseEntry> GetStatusAsync(Guid statusId)
    {
        var status = await statusRepository.GetByIdAsync(statusId)
                     ?? throw new NotFoundException("Status not found.");
        return status.ToResponse();
    }

    public async Task<Guid> CreateAsync(Guid projectId, CreateStatusRequest request)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        var created = await statusRepository.InsertAsync(request.ToTable(projectId))
                      ?? throw new InvalidOperationException("Failed to create status.");
        return created.Id;
    }

    public async Task UpdateAsync(Guid statusId, UpdateStatusRequest request)
    {
        if (await statusRepository.GetByIdAsync(statusId) is null)
            throw new NotFoundException("Status not found.");

        _ = await statusRepository.UpdateAsync(statusId, request.Name, request.Position)
            ?? throw new NotFoundException("Status not found.");
    }

    public async Task DeleteAsync(Guid statusId)
    {
        if (await statusRepository.GetByIdAsync(statusId) is null)
            throw new NotFoundException("Status not found.");

        await statusRepository.DeleteAsync(statusId);
    }
}