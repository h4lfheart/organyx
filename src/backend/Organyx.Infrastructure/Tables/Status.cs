using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Organyx.Infrastructure.Tables;

[Table("status")]
public class Status : BaseModel
{
    [PrimaryKey("id", shouldInsert: false)]
    public Guid Id { get; set; }

    [Column("project_id")] public Guid ProjectId { get; set; }
    [Column("name")] public string Name { get; set; } = string.Empty;
    [Column("position")] public int Position { get; set; }
    [Column("is_default")] public bool IsDefault { get; set; }
}
