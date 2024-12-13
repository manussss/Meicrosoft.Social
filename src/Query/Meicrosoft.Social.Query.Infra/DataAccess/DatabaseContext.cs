namespace Meicrosoft.Social.Query.Infra.DataAccess;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
}
