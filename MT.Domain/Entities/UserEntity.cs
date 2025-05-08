namespace MT.Domain.Entities;

public class UserEntity: BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool isBanned { get; set; }
    public bool isVerified { get; set; }
    
    public virtual ContactsEntity Contacts { get; set; } = new ContactsEntity();
    public virtual SocialsEntity Socials { get; set; } = new SocialsEntity();
    public virtual ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    public virtual PersonalsEntity Personals { get; set; } = new PersonalsEntity();
}