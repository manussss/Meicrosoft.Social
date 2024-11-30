namespace Meicrosoft.Social.Command.API.Events;

public class MessageUpdatedEvent : BaseEvent
{
    public string Message { get; set; }

    public MessageUpdatedEvent() : base(nameof(MessageUpdatedEvent))
    {
    }
}
