namespace MT.Contracts.Users;

public class UserResponse
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public ContactsDto Contacts { get; set; } = new();
    public SocialsDto Socials { get; set; } = new();
    public PersonalsDto Personals { get; set; } = new();
}
