namespace Finisher.Domain.Constants;

public abstract class UserPolicies
{
    public const string CanPurge = nameof(CanPurge);
    public const string CanCreate = nameof(CanCreate);
    public const string CanUpdate = nameof(CanUpdate);
    public const string CanDelete = nameof(CanDelete);
    public const string CanView = nameof(CanView);
    public const string CanIdentity = nameof(CanIdentity);

    public static string[] GetPoliciesForCreate()
    {
        return [CanCreate];
    }

    public static string[] GetPoliciesForUpdate()
    {
        return [CanUpdate];
    }

    public static string[] GetPoliciesForDelete()
    {
        return [CanDelete];
    }

    public static string[] GetPoliciesForView()
    {
        return [CanView];
    }

    public static string[] GetPoliciesForIdentity()
    {
        return [CanIdentity];
    }

    public static string[] GetPoliciesForUser()
    {
        return [CanIdentity, CanView];
    }

    public static string[] GetPoliciesForModerator()
    {
        return [CanIdentity, CanView, CanCreate, CanUpdate];
    }

    public static string[] GetPoliciesForAdmin()
    {
        return [CanIdentity, CanView, CanCreate, CanUpdate, CanPurge, CanDelete];
    }
}
