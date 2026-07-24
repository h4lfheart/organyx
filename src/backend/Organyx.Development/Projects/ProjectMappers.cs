using Organyx.Development.Projects.Models;
using Organyx.Development.Projects.Tables;

namespace Organyx.Development.Projects;

public static class ProjectMappers
{
    extension(CreateProjectRequest request)
    {
        public Project ToTable(string slug) => new()
        {
            Key = request.Key.Trim().ToUpperInvariant(),
            Slug = slug,
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
            Slug = project.Slug,
            Name = project.Name,
            Description = project.Description
        };
    }
}
