namespace Meicrosoft.Social.Query.Infra.Handlers;

public class EventHandler(
    IPostRepository postRepository, 
    ICommentRepository commentRepository) : IEventHandler
{
    public async Task On(PostCreatedEvent @event)
    {
        var post = new Post
        {
            Id = @event.Id,
            Author = @event.Author,
            DatePosted = @event.DatePosted,
            Message = @event.Message,
        };

        await postRepository.CreateAsync(post);
    }

    public async Task On(MessageUpdatedEvent @event)
    {
        var post = await postRepository.GetByIdAsync(@event.Id);

        if (post == null)
            return;

        post.Message = @event.Message;

        await postRepository.UpdateAsync(post);
    }

    public async Task On(PostLikedEvent @event)
    {
        var post = await postRepository.GetByIdAsync(@event.Id);

        if (post == null)
            return;

        post.Likes++;

        await postRepository.UpdateAsync(post);
    }

    public async Task On(CommentAddedEvent @event)
    {
        var comment = new Comment
        {
            PostId = @event.Id,
            Id = @event.CommentId,
            CommentDate = @event.CommentDate,
            CommentText = @event.Comment,
            Username = @event.Username,
            Edited = false
        };

        await commentRepository.CreateAsync(comment);
    }

    public async Task On(CommentUpdatedEvent @event)
    {
        var comment = await commentRepository.GetByIdAsync(@event.CommentId);

        if (comment == null)
            return;

        comment.CommentText = @event.Comment;
        comment.Edited = true;
        comment.CommentDate = @event.EditDate;

        await commentRepository.UpdateAsync(comment);
    }

    public async Task On(CommentRemovedEvent @event)
    {
        await commentRepository.DeleteAsync(@event.CommentId);
    }

    public async Task On(PostRemovedEvent @event)
    {
        await postRepository.DeleteAsync(@event.Id);
    }
}
