namespace Organyx.Application.Tasks.Models;

public enum Priority
{
    Low,
    Medium,
    High,
    Urgent
}

public static class PriorityMapping
{
    public static string ToDatabase(Priority priority) => priority switch
    {
        Priority.Low => "low",
        Priority.Medium => "medium",
        Priority.High => "high",
        Priority.Urgent => "urgent",
        _ => "medium"
    };

    public static Priority FromDatabase(string priority) => priority switch
    {
        "low" => Priority.Low,
        "medium" => Priority.Medium,
        "high" => Priority.High,
        "urgent" => Priority.Urgent,
        _ => Priority.Medium
    };
}
