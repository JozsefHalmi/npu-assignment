namespace Reviews.Domain.Events;
public class ReviewCreatedEvent : BaseEvent
{
    public Review Review { get; set; }

    public ReviewCreatedEvent(Review creation)
    {
        Review = creation;
    }
}
