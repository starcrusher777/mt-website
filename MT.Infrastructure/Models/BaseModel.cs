namespace MT.Infrastructure.Models;

public class BaseModel
{
    public long Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}