using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Infrastructure.Tables;

[Table("tasks")]
public class ProjectTask : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid Id { get; set; }

    [Column("project_id")] public Guid ProjectId { get; set; }
    [Column("feature_id")] public Guid? FeatureId { get; set; }
    [Column("status_id")] public Guid StatusId { get; set; }

    [Column("number", ignoreOnInsert: true)]
    public int Number { get; set; }

    [Column("title")] public string Title { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }
    [Column("priority")] public string Priority { get; set; } = "medium";

    [Column("created_at", ignoreOnInsert: true, ignoreOnUpdate: true)]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("updated_at", ignoreOnInsert: true, ignoreOnUpdate: true)]
    public DateTimeOffset UpdatedAt { get; set; }
}
