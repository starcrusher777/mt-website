namespace MT.Infrastructure.Models;

public class UserUpdateModel : BaseModel
{
    public ContactsModel Contacts { get; set; }
    public SocialsModel Socials { get; set; }
    public PersonalsModel Personals { get; set; }
}