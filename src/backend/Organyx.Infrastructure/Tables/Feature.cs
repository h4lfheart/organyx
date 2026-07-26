using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Infrastructure.Tables;

[Table("features")]
public class Feature : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid Id { get; set; }

    [Column("project_id")] public Guid ProjectId { get; set; }
    [Column("status_id")] public Guid? StatusId { get; set; }
    [Column("slug")] public string Slug { get; set; } = string.Empty;
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }

    [Column("created_at", ignoreOnInsert: true, ignoreOnUpdate: true)]
    public DateTimeOffset CreatedAt { get; set; }

    [Column("updated_at", ignoreOnInsert: true, ignoreOnUpdate: true)]
    public DateTimeOffset UpdatedAt { get; set; }
}
