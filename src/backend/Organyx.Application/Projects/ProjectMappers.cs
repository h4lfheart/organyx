using Organyx.Application.Projects.Models;
using Organyx.Application.Projects.Tables;

namespace Organyx.Application.Projects;

public static class ProjectMappers
{
    extension(CreateProjectRequest request)
    {
        public Project ToTable() => new()
        {
            Key = request.Key.Trim().ToUpperInvariant(),
            Name = request.Name,
            Description = request.Description
        };
    }

    extension(Project project)
    {
        public ProjectResponseEntry ToResponse() => new()
        {
            Id = project.Id,
            Key = project.Key,
            Name = project.Name,
            Description = project.Description
        };
    }
}
