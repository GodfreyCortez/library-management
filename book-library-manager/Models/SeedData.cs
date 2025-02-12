using Raven.Client.Documents;
using Raven.Client.Documents.Operations;
using Raven.Client.Documents.Session;

namespace book_library_manager.Models;

public static class SeedData
{
    public static void Initialize(IDocumentStore store)
    {
        RavenDbContext.CreateDatabaseIfNotExists();
        /**
            Clear database for seeded data
        */
        var deleteByQueryOp = new DeleteByQueryOperation("from 'Books'");

        // Execute the operation by passing it to Operations.Send
        store.Operations.Send(deleteByQueryOp);
    }
}