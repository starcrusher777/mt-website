namespace MT.Infrastructure.Models;

public class UserViewModel : BaseModel
{
    public long UserId { get; set; }
    public string Username { get; set; }
    public bool IsBanned { get; set; }
    public bool IsVerified { get; set; }
    public ContactsModel Contacts { get; set; }
    public SocialsModel Socials { get; set; }
    public OrderModel Order { get; set; }
    public PersonalsModel Personals { get; set; }
}