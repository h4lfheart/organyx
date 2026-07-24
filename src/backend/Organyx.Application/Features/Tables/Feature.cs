using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Application.Features.Tables;

[Table("features")]
public class Feature : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)] public string Id { get; set; } = string.Empty;
    [Column("project_id")] public string ProjectId { get; set; } = string.Empty;
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }
}
