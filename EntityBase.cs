﻿using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace SharedKernel;

// This can be modified to EntityBase<TId> to support multiple key types (e.g. Guid)
public abstract class EntityBase<TId>
{
    public TId Id { get; set; } = default!;

    private List<DomainEventBase> _domainEvents = new ();
    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
    internal void ClearDomainEvents() => _domainEvents.Clear();
}