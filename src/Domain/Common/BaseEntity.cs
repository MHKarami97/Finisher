using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Finisher.Domain.Common;

public abstract class BaseEntity : BaseEntity<int>;

public abstract class BaseEntity<T>
{
    public T Id { get; protected set; } = default!;

    private readonly List<BaseEvent> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        var other = (BaseEntity<T>)obj;

        return !IsTransient() && !other.IsTransient() && Id!.Equals(other.Id);
    }

    public static bool operator ==(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        return left is null && right is null || left is not null && right is not null && left.Equals(right);
    }

    public static bool operator !=(BaseEntity<T>? left, BaseEntity<T>? right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Returns true if the entity is transient (Id equals default value).
    /// </summary>
    public bool IsTransient()
    {
        return EqualityComparer<T>.Default.Equals(Id, default!);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            return Id!.GetHashCode() ^ 31;
        }

        return RuntimeHelpers.GetHashCode(this);
    }
}
