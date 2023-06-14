using MassTransit;
namespace SharedKernel;

public class DomainMessageDispatcher
{
    private IPublishEndpoint _publishEndpoint { get; set; }
    
    public DomainMessageDispatcher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }
    
    public async Task DispatchAndClearMessages(IEnumerable<EntityBase<Guid>> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await _publishEndpoint.Publish(domainEvent);
            }
        }
    }
}