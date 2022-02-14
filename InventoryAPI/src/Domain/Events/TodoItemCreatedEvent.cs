namespace InventoryAPI.Domain.Events;

public class TodoItemCreatedEvent : DomainEvent
{
    public TodoItemCreatedEvent(TyrePrice item)
    {
        Item = item;
    }

    public TyrePrice Item { get; }
}