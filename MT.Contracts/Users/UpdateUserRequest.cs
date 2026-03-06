namespace MT.Contracts.Users;

public class UpdateUserRequest
{
    public ContactsDto Contacts { get; set; } = new();
    public SocialsDto Socials { get; set; } = new();
    public PersonalsDto Personals { get; set; } = new();
}
