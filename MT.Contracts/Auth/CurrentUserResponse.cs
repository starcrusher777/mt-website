namespace MT.Contracts.Auth;

public class CurrentUserResponse
{
    public string Username { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Id { get; set; }
}
