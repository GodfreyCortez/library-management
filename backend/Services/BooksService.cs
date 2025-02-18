using System.Runtime.InteropServices.Marshalling;
using book_library_manager.Models;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace book_library_manager.Services;


public class BooksService
{
    public async Task<List<Book>> GetBooks()
    {
        RavenDbContext.CreateDatabaseIfNotExists();

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

    public async Task<Book> GetBook(BookIdentifier bookId)
    {
        using (IAsyncDocumentSession session = RavenDbContext.Store.OpenAsyncSession())
        {
            var book = await session.LoadAsync<Book>($"Books/{bookId.Id}");

            return book;
        }
    }

    public async Task DeleteBook(BookIdentifier bookId)
    {
        using (IAsyncDocumentSession session = RavenDbContext.Store.OpenAsyncSession())
        {
            var book = await session.LoadAsync<Book>($"{bookId.Id}");

            session.Delete(book);
            await session.SaveChangesAsync();
        }
    }
}