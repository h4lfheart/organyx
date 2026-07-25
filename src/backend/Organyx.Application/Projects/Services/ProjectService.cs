using Organyx.Application.Projects.Models;
using Organyx.Application.Projects.Repositories;

namespace Organyx.Application.Projects.Services;

public interface IProjectService
{
    Task<ProjectsResponse> GetProjectsAsync();
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
}
