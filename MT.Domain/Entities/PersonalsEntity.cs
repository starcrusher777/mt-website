using System.ComponentModel.DataAnnotations.Schema;

namespace MT.Domain.Entities;

public class PersonalsEntity : BaseEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }
}