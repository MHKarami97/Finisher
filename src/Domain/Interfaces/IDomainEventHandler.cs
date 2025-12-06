namespace Finisher.Domain.Interfaces;

public interface IDomainEventHandler<in TEvent> where TEvent : BaseEvent
{
    Task HandleAsync(TEvent domainEvent);
}
