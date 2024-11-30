namespace Meicrosoft.Social.Command.API.Events;

public class CommentRemovedEvent : BaseEvent
{
    public Guid CommentId { get; set; }

    public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
    {
    }
}
