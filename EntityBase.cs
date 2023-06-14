using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace SharedKernel;

// This can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
public abstract class EntityBase<TId>
{
    public TId Id { get; set; } = default!;

    private List<DomainEventBase> _domainEvents = new ();
    private List<IDomainMessageBase> _domainMessages = new ();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
    [NotMapped]
    public IEnumerable<IDomainMessageBase> DomainMessages => _domainMessages.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    protected void RegisterDomainMessage(IDomainMessageBase domainMessage) => _domainMessages.Add(domainMessage);
    internal void ClearDomainEvents() => _domainEvents.Clear();
}