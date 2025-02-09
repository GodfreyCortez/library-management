using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Exceptions.Database;
using Raven.Client.ServerWide;
using Raven.Client.ServerWide.Operations;

public class RavenDbContext
{
    private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);
    private static string DatabaseName = "book_library_manager";
    public static IDocumentStore Store => store.Value;

    private static IDocumentStore CreateStore()
    {
        var builder = WebApplication.CreateBuilder();
        IDocumentStore store = new DocumentStore()
        {
            // Define the cluster node URLs (required)
            Urls = new[] { builder.Configuration.GetConnectionString("RavenDbContext"), 
                            /*some additional nodes of this cluster*/ },

            // Set conventions as necessary (optional)
            Conventions =
                {
                    MaxNumberOfRequestsPerSession = 10,
                    UseOptimisticConcurrency = true
                },

            // Define a default database (optional)
            Database = DatabaseName,
        }.Initialize();

        return store;
    }

    public static void CreateDatabaseIfNotExists()
    {
        try
        {
            Store.Maintenance.ForDatabase(DatabaseName).Send(new GetStatisticsOperation());
        }
        catch (DatabaseDoesNotExistException)
        {
            Store.Maintenance.Server.Send(new CreateDatabaseOperation(new DatabaseRecord(DatabaseName)));
        }
    }
}