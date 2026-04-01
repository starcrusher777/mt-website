using MT.Domain.Enums;

namespace MT.Domain.Authorization;

/// <summary>String role names for JWT and <see cref="Microsoft.AspNetCore.Authorization.AuthorizeAttribute"/>.</summary>
public static class AppRoles
{
    public const string User = nameof(UserRole.User);
    public const string Moderator = nameof(UserRole.Moderator);
    public const string Administrator = nameof(UserRole.Administrator);

    public const string ModeratorOrAdministrator = $"{Moderator},{Administrator}";
}
