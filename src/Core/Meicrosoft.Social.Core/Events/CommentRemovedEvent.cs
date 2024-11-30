namespace Meicrosoft.Social.Core.Events;

public class CommentRemovedEvent : BaseEvent
{
    public Guid CommentId { get; set; }

    public CommentRemovedEvent() : base(nameof(CommentRemovedEvent))
    {
    }
}
