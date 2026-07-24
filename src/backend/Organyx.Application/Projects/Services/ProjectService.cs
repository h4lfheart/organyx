using System.Net;
using Organyx.Application.Projects.Models;
using Organyx.Application.Projects.Repositories;

namespace Organyx.Application.Projects.Services;

public interface IProjectService
{
    Task<ProjectsResponse> GetProjectsAsync();
    Task<ProjectResponseEntry?> GetProjectAsync(string projectId);
    Task<(HttpStatusCode Status, ProjectResponseEntry? Project)> CreateAsync(CreateProjectRequest request);
    Task<(HttpStatusCode Status, ProjectResponseEntry? Project)> UpdateAsync(string projectId, UpdateProjectRequest request);
    Task<HttpStatusCode> DeleteAsync(string projectId);
}

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    public async Task<ProjectsResponse> GetProjectsAsync()
    {
        var projects = await projectRepository.GetAllAsync();
        return new ProjectsResponse
        {
            Entries = projects.Select(project => project.ToResponse())
        };
    }

    public async Task<ProjectResponseEntry?> GetProjectAsync(string projectId)
    {
        var project = await projectRepository.GetByIdAsync(projectId);
        return project?.ToResponse();
    }

    public async Task<(HttpStatusCode Status, ProjectResponseEntry? Project)> CreateAsync(CreateProjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Key) || string.IsNullOrWhiteSpace(request.Name))
            return (HttpStatusCode.BadRequest, null);

        var key = request.Key.Trim().ToUpperInvariant();
        if (await projectRepository.GetByKeyAsync(key) is not null)
            return (HttpStatusCode.Conflict, null);

        var created = await projectRepository.InsertAsync(request.ToTable());
        return created is not null
            ? (HttpStatusCode.Created, created.ToResponse())
            : (HttpStatusCode.InternalServerError, null);
    }

    public async Task<(HttpStatusCode Status, ProjectResponseEntry? Project)> UpdateAsync(string projectId, UpdateProjectRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return (HttpStatusCode.BadRequest, null);

        if (await projectRepository.GetByIdAsync(projectId) is null)
            return (HttpStatusCode.NotFound, null);

        var updated = await projectRepository.UpdateAsync(projectId, request.Name, request.Description);
        return updated is not null
            ? (HttpStatusCode.OK, updated.ToResponse())
            : (HttpStatusCode.NotFound, null);
    }

    public async Task<HttpStatusCode> DeleteAsync(string projectId)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            return HttpStatusCode.NotFound;

        await projectRepository.DeleteAsync(projectId);
        return HttpStatusCode.NoContent;
    }
}
