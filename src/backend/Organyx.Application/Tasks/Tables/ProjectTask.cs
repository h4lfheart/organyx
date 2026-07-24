using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Application.Tasks.Tables;

[Table("tasks")]
public class ProjectTask : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)] public string Id { get; set; } = string.Empty;
    [Column("project_id")] public string ProjectId { get; set; } = string.Empty;
    [Column("feature_id")] public string? FeatureId { get; set; }
    [Column("status_id")] public string? StatusId { get; set; }
    [Column("number", ignoreOnInsert: true)] public int Number { get; set; }
    [Column("title")] public string Title { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }
    [Column("priority")] public string Priority { get; set; } = "medium";
}
