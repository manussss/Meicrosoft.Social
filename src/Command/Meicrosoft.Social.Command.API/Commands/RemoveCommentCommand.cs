﻿namespace Meicrosoft.Social.Command.API.Commands;

public class RemoveCommentCommand : BaseCommand
{
    public Guid CommentId { get; set; }
    public string Username { get; set; }
}
