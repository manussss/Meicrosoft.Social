namespace Meicrosoft.Social.Query.Infra.DataAccess;

public class DatabaseContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
{
    public DatabaseContext CreateDbContext()
    {
        DbContextOptionsBuilder<DatabaseContext> options = new();

        configureDbContext(options);

        return new DatabaseContext(options.Options);
    }
}
