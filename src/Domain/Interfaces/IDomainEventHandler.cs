namespace Finisher.Domain.Interfaces;

internal interface IDomainEventHandler<in TEvent> where TEvent : BaseEvent
{
    Task HandleAsync(TEvent domainEvent);
}
