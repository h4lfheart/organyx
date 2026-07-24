using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Application.Statuses.Tables;

[Table("status")]
public class Status : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)] public string Id { get; set; } = string.Empty;
    [Column("project_id")] public string ProjectId { get; set; } = string.Empty;
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("position")] public int Position { get; set; }
}
