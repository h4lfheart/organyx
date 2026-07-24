namespace Organyx.Development.Statuses.Models;

public record CreateStatusRequest
{
    public required string Name { get; init; }
    public int Position { get; init; }
}