using System.ComponentModel.DataAnnotations.Schema;

namespace MT.Domain.Entities;

public class SocialsEntity : BaseEntity
{
    public string? Telegram { get; set; }
    public string? Vkontakte { get; set; }
    public string? Instagram { get; set; }
    public string? Twitter { get; set; }
    
    public long UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }
}