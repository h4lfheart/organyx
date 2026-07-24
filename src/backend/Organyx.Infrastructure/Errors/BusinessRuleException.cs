namespace Organyx.Infrastructure.Errors;

public sealed class BusinessRuleException(string message) : Exception(message);
