namespace InventoryAPI.Domain.Events;

public class TodoItemDeletedEvent : DomainEvent
{
    public TodoItemDeletedEvent(TyrePrice item)
    {
        Item = item;
    }

    public TyrePrice Item { get; }
}