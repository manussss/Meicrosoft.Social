﻿namespace Meicrosoft.Social.Command.API.Commands;

public class NewPostCommand : BaseCommand
{
    public string Author { get; set; }
    public string Message { get; set; }
}
