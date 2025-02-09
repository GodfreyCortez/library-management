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

        using (IDocumentSession session = store.OpenSession())
        {

            Book entityOne = new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "The Martian",
                Author = "Andy Weir"
            };

            Book entityTwo = new Book()
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Wool",
                Author = "Hugh Howey"
            };

            session.Store(entityOne);
            session.Store(entityTwo);
            session.SaveChanges();
        }
    }
}