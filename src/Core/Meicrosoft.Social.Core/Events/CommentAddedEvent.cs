namespace Meicrosoft.Social.Core.Events;

public class CommentAddedEvent : BaseEvent
{
    public Guid CommentId { get; set; }
    public string Comment { get; set; }
    public string Username { get; set; }
    public DateTime CommentDate { get; set; }

    public CommentAddedEvent() : base(nameof(CommentAddedEvent))
    {
    }
}
