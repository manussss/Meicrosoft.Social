﻿namespace Meicrosoft.Social.Command.API.Events;

public class PostCreatedEvent : BaseEvent
{
    public string Author { get; set; }
    public string Message { get; set; }
    public DateTime DatePosted { get; set; }

    public PostCreatedEvent() : base(nameof(PostCreatedEvent))
    {
    }
}
