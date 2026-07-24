using Organyx.Application.Features.Models;
using Organyx.Application.Features.Tables;

namespace Organyx.Application.Features;

public static class FeatureMappers
{
    extension(CreateFeatureRequest request)
    {
        public Feature ToTable(string projectId) => new()
        {
            ProjectId = projectId,
            Name = request.Name,
            Description = request.Description
        };
    }

    extension(Feature feature)
    {
        public FeatureResponseEntry ToResponse() => new()
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Name = feature.Name,
            Description = feature.Description
        };
    }
}
