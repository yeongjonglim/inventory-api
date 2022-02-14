using InventoryAPI.Domain.Common;

namespace InventoryAPI.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}