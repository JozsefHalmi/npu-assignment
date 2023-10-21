namespace Creations.Domain.Events;
public class CreationCreatedEvent : BaseEvent
{
    public Creation Creation { get; set; }

    public CreationCreatedEvent(Creation creation)
    {
        Creation = creation;
    }
}
