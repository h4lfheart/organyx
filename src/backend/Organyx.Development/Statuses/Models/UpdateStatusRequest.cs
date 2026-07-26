namespace Organyx.Development.Statuses.Models;

public record UpdateStatusRequest
{
    public required string Name { get; init; }
    public required int Position { get; init; }
    public required bool IsDefault { get; init; }
    public required bool IsComplete { get; init; }
}