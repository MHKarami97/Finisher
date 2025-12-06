namespace Finisher.Domain.Constants;

public abstract class UserRoles
{
    public const string Administrator = nameof(Administrator);
    public const string Moderator = nameof(Moderator);
    public const string User = nameof(User);

    public static string[] GetRolesForCreate()
    {
        return [Administrator, Moderator];
    }

    public static string[] GetRolesForUpdate()
    {
        return [Administrator, Moderator];
    }

    public static string[] GetRolesForDelete()
    {
        return [Administrator, Moderator];
    }

    public static string[] GetRolesForView()
    {
        return [Administrator, Moderator, User];
    }
}
