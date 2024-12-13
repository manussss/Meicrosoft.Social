namespace Meicrosoft.Social.Query.Infra.Repositories;

public class CommentRepository(DatabaseContextFactory databaseContextFactory) : ICommentRepository
{
    public async Task CreateAsync(Comment comment)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        await dbContext.Comments.AddAsync(comment);
        _ = await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        var comment = await GetByIdAsync(id);

        if (comment == null)
            return;

        dbContext.Comments.Remove(comment);
        _ = await dbContext.SaveChangesAsync();
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        return await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateAsync(Comment comment)
    {
        using var dbContext = databaseContextFactory.CreateDbContext();

        dbContext.Comments.Update(comment);
        _ = await dbContext.SaveChangesAsync();
    }
}
