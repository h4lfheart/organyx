namespace Organyx.Application.Statuses.Models;

public record UpdateStatusRequest
{
    public required string Name { get; init; }
    public required int Position { get; init; }
}
