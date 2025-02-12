using System.Runtime.InteropServices.Marshalling;
using book_library_manager.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace book_library_manager.Services;


public class BooksService
{
    public async Task<List<Book>> GetBooks()
    {
        List<Book> allBooks = [];

        using (IAsyncDocumentSession session = RavenDbContext.Store.OpenAsyncSession())
        {
            allBooks.AddRange(await session
                .Query<Book>()
                .ToListAsync()
            );
        }

        return allBooks;
    }

    public async Task CreateBook(Book newBook)
    {
        using (IAsyncDocumentSession session = RavenDbContext.Store.OpenAsyncSession())
        {
            await session.StoreAsync(newBook);
            await session.SaveChangesAsync();
        }

    }
}