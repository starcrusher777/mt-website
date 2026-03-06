namespace MT.Contracts.Auth;

public class LoginResponse
{
    public string Message { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public long UserId { get; set; }
    public string Token { get; set; } = string.Empty;
}
