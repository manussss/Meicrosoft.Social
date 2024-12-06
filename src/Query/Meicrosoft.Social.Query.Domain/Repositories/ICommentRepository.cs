namespace Meicrosoft.Social.Query.Domain.Repositories;

public interface ICommentRepository
{
    Task CreateAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task<Comment> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
}
