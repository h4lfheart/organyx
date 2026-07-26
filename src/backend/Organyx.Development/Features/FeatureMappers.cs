using Organyx.Development.Features.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Development.Features;

public static class FeatureMappers
{
    extension(CreateFeatureRequest request)
    {
        public Feature ToTable(Guid projectId, string slug) => new()
        {
            ProjectId = projectId,
            Slug = slug,
            Name = request.Name,
            Description = request.Description,
            StatusId = request.StatusId
        };
    }

    extension(Feature feature)
    {
        public FeatureResponseEntry ToResponse() => new()
        {
            Id = feature.Id,
            ProjectId = feature.ProjectId,
            Slug = feature.Slug,
            Name = feature.Name,
            Description = feature.Description,
            StatusId = feature.StatusId,
            CreatedAt = feature.CreatedAt,
            UpdatedAt = feature.UpdatedAt
        };
    }
}
