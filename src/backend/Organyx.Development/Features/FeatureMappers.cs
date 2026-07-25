using Organyx.Development.Features.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Development.Features;

public static class FeatureMappers
{
    extension(CreateFeatureRequest request)
    {
        public Feature ToTable(Guid projectId) => new()
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