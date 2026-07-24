using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Development.Statuses.Tables;

[Table("status")]
public class Status : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid Id { get; set; }

    [Column("project_id")] public Guid ProjectId { get; set; }
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("position")] public int Position { get; set; }
}