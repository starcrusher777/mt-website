namespace MT.Infrastructure.Models;

public class UserModel : BaseModel
{
    public long UserId { get; set; }
    public string Username { get; set; }
    private string Password { get; set; }
    public bool isBanned { get; set; }
    public bool isVerified { get; set; }
    
    public ContactsModel Contacts { get; set; }
    public SocialsModel Socials { get; set; }
    public OrderModel Order { get; set; }
    public PersonalsModel Personals { get; set; }
}