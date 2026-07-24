using Organyx.Development.Statuses.Models;
using Organyx.Development.Statuses.Tables;

namespace Organyx.Development.Statuses;

public static class StatusMappers
{
    extension(CreateStatusRequest request)
    {
        public Status ToTable(Guid projectId) => new()
        {
            ProjectId = projectId,
            Name = request.Name,
            Position = request.Position
        };
    }

    extension(Status status)
    {
        public StatusResponseEntry ToResponse() => new()
        {
            Id = status.Id,
            ProjectId = status.ProjectId,
            Name = status.Name,
            Position = status.Position
        };
    }
}