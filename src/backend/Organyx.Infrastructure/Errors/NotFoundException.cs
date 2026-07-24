namespace Organyx.Infrastructure.Errors;

public sealed class NotFoundException(string message) : Exception(message);
