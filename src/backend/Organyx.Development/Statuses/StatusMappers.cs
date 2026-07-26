using Organyx.Development.Statuses.Models;
using Organyx.Infrastructure.Tables;

namespace Organyx.Development.Statuses;

public static class StatusMappers
{
    extension(CreateStatusRequest request)
    {
        public Status ToTable(Guid projectId) => new()
        {
            ProjectId = projectId,
            Name = request.Name,
            Position = request.Position,
            IsDefault = request.IsDefault,
            IsComplete = request.IsComplete
        };
    }

    extension(Status status)
    {
        public StatusResponseEntry ToResponse() => new()
        {
            Id = status.Id,
            ProjectId = status.ProjectId,
            Name = status.Name,
            Position = status.Position,
            IsDefault = status.IsDefault,
            IsComplete = status.IsComplete
        };
    }
}