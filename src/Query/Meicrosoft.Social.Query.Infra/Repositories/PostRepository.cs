namespace Meicrosoft.Social.Query.Infra.Repositories;

public class PostRepository(DatabaseContextFactory databaseContextFactory) : IPostRepository
{
    public async Task CreateAsync(Post post)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        await dbContext.Posts.AddAsync(post);
        _ = await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        var post = await GetByIdAsync(id);

        if (post == null)
            return;

        dbContext.Posts.Remove(post);
        _ = await dbContext.SaveChangesAsync();
    }

    public async Task<List<Post>> GetAllAsync()
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        return await dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .ToListAsync();
    }

    public async Task<List<Post>> GetByAuthorAsync(string author)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        return await dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .Where(p => p.Author.Contains(author))
            .ToListAsync();
    }

    public async Task<Post> GetByIdAsync(Guid id)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        return await dbContext.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Post>> GetByLikesAsync(int numberOfLikes)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        return await dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .Where(p => p.Likes >= numberOfLikes)
            .ToListAsync();
    }

    public async Task<List<Post>> GetWithCommentsAsync()
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        return await dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Comments)
            .Where(p => p.Comments != null && p.Comments.Any())
            .ToListAsync();
    }

    public async Task UpdateAsync(Post post)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        dbContext.Posts.Update(post);
        _ = await dbContext.SaveChangesAsync();
    }
}
