namespace Finisher.Domain.Constants;

public abstract class UserPermissions
{
    public const string CanCreate = nameof(CanCreate);
    public const string CanUpdate = nameof(CanUpdate);
    public const string CanDelete = nameof(CanDelete);
    public const string CanView = nameof(CanView);
    public const string CanIdentity = nameof(CanIdentity);

    public static string[] GetPermissionsForCreate()
    {
        return [CanCreate];
    }

    public static string[] GetPermissionsForUpdate()
    {
        return [CanUpdate];
    }

    public static string[] GetPermissionsForDelete()
    {
        return [CanDelete];
    }

    public static string[] GetPermissionsForView()
    {
        return [CanView];
    }

    public static string[] GetPermissionsForIdentity()
    {
        return [CanIdentity];
    }

    public static string[] GetPermissionsForUser()
    {
        return [CanIdentity, CanView];
    }

    public static string[] GetPermissionsForModerator()
    {
        return [CanIdentity, CanView, CanCreate, CanUpdate];
    }

    public static string[] GetPermissionsForAdmin()
    {
        return [CanIdentity, CanView, CanCreate, CanUpdate, CanDelete];
    }
}
