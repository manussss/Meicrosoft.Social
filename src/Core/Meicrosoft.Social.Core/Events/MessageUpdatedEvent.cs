namespace Meicrosoft.Social.Core.Events;

public class MessageUpdatedEvent : BaseEvent
{
    public string Message { get; set; }

    public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
    {
    }
}
