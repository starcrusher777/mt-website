namespace MT.Domain.Entities;

public class UserEntity: BaseEntity
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool isBanned { get; set; }
    public bool isVerified { get; set; }
    
    public virtual ContactsEntity Contacts { get; set; } = new ContactsEntity();
    public virtual SocialsEntity Socials { get; set; } = new SocialsEntity();
    public virtual ICollection<OrderEntity> Order { get; set; } = new List<OrderEntity>();
    public virtual PersonalsEntity Personals { get; set; } = new PersonalsEntity();
}