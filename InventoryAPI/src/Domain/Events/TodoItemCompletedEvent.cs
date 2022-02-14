namespace InventoryAPI.Domain.Events;

public class TodoItemCompletedEvent : DomainEvent
{
    public TodoItemCompletedEvent(TyrePrice item)
    {
        Item = item;
    }

    public TyrePrice Item { get; }
}