namespace MT.Contracts.Users;

public class CreateUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public ContactsDto Contacts { get; set; } = new();
    public SocialsDto Socials { get; set; } = new();
    public PersonalsDto Personals { get; set; } = new();
}
