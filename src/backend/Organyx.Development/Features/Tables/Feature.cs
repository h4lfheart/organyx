using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Development.Features.Tables;

[Table("features")]
public class Feature : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid Id { get; set; }

    [Column("project_id")] public Guid ProjectId { get; set; }
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }
}