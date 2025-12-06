namespace Finisher.Domain.Extensions;

public static class EventExtensions
{
    public static void RaiseEvent(this BaseEntity entity, BaseEvent domainEvent)
    {
        entity.AddDomainEvent(domainEvent);
    }

    public static void ClearEvents(this BaseEntity entity)
    {
        entity.ClearDomainEvents();
    }
}
