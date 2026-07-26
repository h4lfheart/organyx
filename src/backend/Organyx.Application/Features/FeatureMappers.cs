using Organyx.Application.Features.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Application.Features;

public static class FeatureMappers
{
    extension(Feature feature)
    {
        public FeatureResponseEntry ToResponse(Status? status) => new()
        {
            Id = feature.Id,
            Slug = feature.Slug,
            Name = feature.Name,
            Description = feature.Description,
            Status = status?.ToBadge(),
            CreatedAt = feature.CreatedAt,
            UpdatedAt = feature.UpdatedAt
        };
    }

    extension(Status status)
    {
        public FeatureStatusBadge ToBadge() => new()
        {
            Id = status.Id,
            Name = status.Name
        };
    }
}
