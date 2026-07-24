namespace Organyx.Infrastructure.Errors;

public sealed class ConflictException(string message) : Exception(message);
