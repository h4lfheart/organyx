using Organyx.Application.Projects.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Application.Projects;

public static class ProjectMappers
{
    extension(Project project)
    {
        public ProjectResponseEntry ToResponse() => new()
        {
            Id = project.Id,
            Key = project.Key,
            Slug = project.Slug,
            Name = project.Name
        };
    }
}
