using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Application.Projects.Tables;

[Table("projects")]
public class Project : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)] public string Id { get; set; } = string.Empty;
    [Column("key")] public string Key { get; set; } = string.Empty;
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("description")] public string? Description { get; set; }
    [Column("task_seq", ignoreOnInsert: true, ignoreOnUpdate: true)] public int TaskSeq { get; set; }
}
