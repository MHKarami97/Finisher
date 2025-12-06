namespace Finisher.Domain.Common;

public abstract class BaseSoftDeleteEntity : BaseSoftDeleteEntity<int>;

public abstract class BaseSoftDeleteEntity<T> : BaseAuditableEntity<T>
{
    public DateTimeOffset? DeletionTime { get; private set; }
    public int? DeleteByUserId { get; private set; }
    public bool IsDeleted { get; private set; }

    public void SetDeletionTime(DateTimeOffset createdTime)
    {
        Guard.AgainstDefaultValue(createdTime, nameof(createdTime));

        DeletionTime = createdTime;
    }

    public void SetDeleteByUserId(int createdByUserId)
    {
        Guard.AgainstNegativeOrZero(createdByUserId, nameof(createdByUserId));

        DeleteByUserId = createdByUserId;
    }

    public void SetIsDeleted(bool isDelete)
    {
        IsDeleted = isDelete;
    }
}
