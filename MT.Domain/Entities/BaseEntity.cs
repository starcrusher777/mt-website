using System.ComponentModel.DataAnnotations;

namespace MT.Domain.Entities;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; }
}
