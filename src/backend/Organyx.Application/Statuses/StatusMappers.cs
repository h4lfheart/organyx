using Organyx.Application.Statuses.Models;
using Organyx.Application.Statuses.Tables;

namespace Organyx.Application.Statuses;

public static class StatusMappers
{
    extension(CreateStatusRequest request)
    {
        public Status ToTable(string projectId) => new()
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
