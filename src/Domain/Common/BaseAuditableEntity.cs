namespace Finisher.Domain.Common;

public abstract class BaseAuditableEntity : BaseAuditableEntity<int>;

public abstract class BaseAuditableEntity<T> : BaseEntity<T>
{
    public DateTimeOffset CreatedTime { get; private set; }

    public int? CreatedByUserId { get; private set; }

    public void SetCreatedTime(DateTimeOffset createdTime)
    {
        Guard.AgainstDefaultValue(createdTime, nameof(createdTime));

        CreatedTime = createdTime;
    }

    public void SetCreatedByUserId(int createdByUserId)
    {
        Guard.AgainstNegativeOrZero(createdByUserId, nameof(createdByUserId));

        CreatedByUserId = createdByUserId!;
    }
}
