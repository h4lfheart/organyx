using Organyx.Development.Projects.Models;
using Organyx.Development.Projects.Repositories;
using Organyx.Infrastructure.Errors;

namespace Organyx.Development.Projects.Services;

public interface IProjectService
{
    Task<ProjectsResponse> GetProjectsAsync();
    Task<ProjectResponseEntry> GetProjectAsync(Guid projectId);
    Task<Guid> CreateAsync(CreateProjectRequest request);
    Task UpdateAsync(Guid projectId, UpdateProjectRequest request);
    Task DeleteAsync(Guid projectId);
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

    public async Task<ProjectResponseEntry> GetProjectAsync(Guid projectId)
    {
        var project = await projectRepository.GetByIdAsync(projectId)
                      ?? throw new NotFoundException("Project not found.");
        return project.ToResponse();
    }

    public async Task<Guid> CreateAsync(CreateProjectRequest request)
    {
        var key = request.Key.Trim().ToUpperInvariant();
        if (await projectRepository.GetByKeyAsync(key) is not null)
            throw new ConflictException($"Project key '{key}' already exists.");

        var created = await projectRepository.InsertAsync(request.ToTable())
                      ?? throw new InvalidOperationException("Failed to create project.");
        return created.Id;
    }

    public async Task UpdateAsync(Guid projectId, UpdateProjectRequest request)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        _ = await projectRepository.UpdateAsync(projectId, request.Name, request.Description)
            ?? throw new NotFoundException("Project not found.");
    }

    public async Task DeleteAsync(Guid projectId)
    {
        if (await projectRepository.GetByIdAsync(projectId) is null)
            throw new NotFoundException("Project not found.");

        await projectRepository.DeleteAsync(projectId);
    }
}