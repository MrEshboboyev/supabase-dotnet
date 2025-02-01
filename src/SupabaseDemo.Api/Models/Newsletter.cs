using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SupabaseDemo.Api.Models;

[Table("newsletters")]
public class Newsletter : BaseModel
{
    [PrimaryKey("id", false)]
    public long Id { get; set; }
    
    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("read-time")] 
    public int ReadTime { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}