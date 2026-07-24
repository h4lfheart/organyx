using System.Net;
using Organyx.Application.Projects.Repositories;
using Organyx.Application.Statuses.Models;
using Organyx.Application.Statuses.Repositories;

namespace Organyx.Application.Statuses.Services;

public interface IStatusService
{
    Task<StatusesResponse?> GetStatusesAsync(string projectId);
    Task<StatusResponseEntry?> GetStatusAsync(string projectId, string statusId);
    Task<(HttpStatusCode Status, StatusResponseEntry? Entry)> CreateAsync(string projectId, CreateStatusRequest request);
    Task<(HttpStatusCode Status, StatusResponseEntry? Entry)> UpdateAsync(string projectId, string statusId, UpdateStatusRequest request);
    Task<HttpStatusCode> DeleteAsync(string projectId, string statusId);
}

public class StatusService(
    IStatusRepository statusRepository,
    IProjectRepository projectRepository
) : IStatusService
{
    public async Task<StatusesResponse?> GetStatusesAsync(string projectId)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            return null;

        var statuses = await statusRepository.GetByProjectIdAsync(projectId);
        return new StatusesResponse
        {
            Entries = statuses.Select(status => status.ToResponse())
        };
    }

    public async Task<StatusResponseEntry?> GetStatusAsync(string projectId, string statusId)
    {
        var status = await statusRepository.GetByIdAsync(statusId);
        if (status is null || status.ProjectId != projectId)
            return null;

        return status.ToResponse();
    }

    public async Task<(HttpStatusCode Status, StatusResponseEntry? Entry)> CreateAsync(string projectId, CreateStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return (HttpStatusCode.BadRequest, null);

        if (await projectRepository.GetByIdAsync(projectId) is null)
            return (HttpStatusCode.NotFound, null);

        var created = await statusRepository.InsertAsync(request.ToTable(projectId));
        return created is not null
            ? (HttpStatusCode.Created, created.ToResponse())
            : (HttpStatusCode.InternalServerError, null);
    }

    public async Task<(HttpStatusCode Status, StatusResponseEntry? Entry)> UpdateAsync(
        string projectId,
        string statusId,
        UpdateStatusRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return (HttpStatusCode.BadRequest, null);

        var existing = await statusRepository.GetByIdAsync(statusId);
        if (existing is null || existing.ProjectId != projectId)
            return (HttpStatusCode.NotFound, null);

        var updated = await statusRepository.UpdateAsync(statusId, request.Name, request.Position);
        return updated is not null
            ? (HttpStatusCode.OK, updated.ToResponse())
            : (HttpStatusCode.NotFound, null);
    }

    public async Task<HttpStatusCode> DeleteAsync(string projectId, string statusId)
    {
        var existing = await statusRepository.GetByIdAsync(statusId);
        if (existing is null || existing.ProjectId != projectId)
            return HttpStatusCode.NotFound;

        await statusRepository.DeleteAsync(statusId);
        return HttpStatusCode.NoContent;
    }
}
