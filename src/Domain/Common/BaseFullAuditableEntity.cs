namespace Finisher.Domain.Common;

public abstract class BaseFullAuditableEntity : BaseFullAuditableEntity<int>;

public abstract class BaseFullAuditableEntity<T> : BaseAuditableEntity<T>
{
    public DateTimeOffset? LastModifiedTime { get; private set; }

    public int? LastModifiedByUserId { get; private set; }

    public void SetLastModifiedTime(DateTimeOffset lastModifiedTime)
    {
        Guard.AgainstDefaultValue(lastModifiedTime, nameof(lastModifiedTime));

        LastModifiedTime = lastModifiedTime;
    }

    public void SetLastModifiedByUserId(int lastModifiedByUserId)
    {
        Guard.AgainstNegativeOrZero(lastModifiedByUserId, nameof(lastModifiedByUserId));

        LastModifiedByUserId = lastModifiedByUserId;
    }
}
