
using System.ComponentModel.DataAnnotations.Schema;

namespace MT.Domain.Entities;

public class ContactsEntity: BaseEntity
{
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    
    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }
}