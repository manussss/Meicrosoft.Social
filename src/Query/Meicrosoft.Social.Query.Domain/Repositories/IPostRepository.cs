namespace Meicrosoft.Social.Query.Domain.Repositories;

public interface IPostRepository
{
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Guid id);
    Task<Post> GetByIdAsync(Guid id);
    Task<List<Post>> GetAllAsync();
    Task<List<Post>> GetByAuthorAsync(string author);
    Task<List<Post>> GetByLikesAsync(int numberOfLikes);
    Task<List<Post>> GetWithCommentsAsync();
}
