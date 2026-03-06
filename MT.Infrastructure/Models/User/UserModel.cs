namespace MT.Infrastructure.Models;

public class UserModel : BaseModel
{
    
    public string Username { get; set; }
    public string Password { get; set; }
    
    public ContactsModel Contacts { get; set; }
    public SocialsModel Socials { get; set; }
    public PersonalsModel Personals { get; set; }
}