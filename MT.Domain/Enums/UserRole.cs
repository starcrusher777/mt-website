namespace MT.Domain.Enums;

/// <summary>Application roles; stored as int on <see cref="Entities.UserEntity"/>.</summary>
public enum UserRole
{
    User = 0,
    Moderator = 1,
    Administrator = 2
}
