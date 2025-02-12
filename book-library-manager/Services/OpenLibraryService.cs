using book_library_manager.Models;
using Newtonsoft.Json.Linq;
using OpenLibraryNET;
using OpenLibraryNET.Data;

namespace book_library_manager.Services;

/**
    Class to manage API calls with openlibrary.org
*/
public class OpenLibraryService
{
    private OpenLibraryClient client = new OpenLibraryClient();

    private Book MapSearchOLWorkDataToBook(OLWorkData workData)
    {
        var authors = GetListFromExtensionData(workData.ExtensionData, "author_name");
        var authorKeys = GetListFromExtensionData(workData.ExtensionData, "author_key");
        var firstPublishYear = GetStringFromExtensionData(workData.ExtensionData, "first_publish_year");
        var description = GetStringFromExtensionData(workData.ExtensionData, "description");

        return new Book()
        {
            Id = workData.ID,
            Title = workData.Title,
            Authors = authors,
            AuthorKeys = authorKeys,
            FirstPublishYear = firstPublishYear,
            Description = description
        };
    }

    private Book MapOLWorkToBook(OLWork work)
    {

        return new Book()
        {
            Id = work.ID
        };
    }

    private List<string> GetListFromExtensionData(IReadOnlyDictionary<string, JToken>? extensionData, string key)
    {
        if (extensionData != null && extensionData.TryGetValue(key, out JToken? token))
        {
            return token.ToObject<List<string>>() ?? new List<string>();
        }
        return new List<string>();
    }

    private string? GetStringFromExtensionData(IReadOnlyDictionary<string, JToken>? extensionData, string key)
    {
        if (extensionData != null && extensionData.TryGetValue(key, out JToken? token))
        {
            return token.ToString();
        }
        return null;
    }

    public async Task<List<Book>> SearchWorks(string searchString)
    {
        try
        {
            // Fixed set of search parameters 
            var searchParameters = new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("fields", "key,title,author_name,author_key,first_publish_year"),
                    new KeyValuePair<string, string>("limit", "50")
                };
            var works = await client.Search.GetSearchResultsAsync(searchString, searchParameters);
            if (works != null && works.Any())
            {
                return works.Select(MapSearchOLWorkDataToBook).ToList();
            }
            return new List<Book>();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            return new List<Book>();
        }
    }

    /**
    Given a book identifier, retrieves information about that 
    book and stores it into the database
    */
    public async Task<Book?> GetBook(BookIdentifier bookIdentifier)
    {
        // First retrieve information about that book
        OLWorkData? workData = null;
        try
        {
            workData = await client.Work.GetDataAsync(bookIdentifier.Id);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred when attempting to find the Work: {e.Message}");
        }

        if (workData == null)
        {
            return null;
        }

        /**
        Second retrieve author information and the publishing year
        to get all data about the book
        */
        List<Book> booksWithAuthorData = new List<Book>();
        try
        {
            List<Book> foundBooks = await SearchWorks(bookIdentifier.Id);
            booksWithAuthorData = booksWithAuthorData.Concat(foundBooks).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred when attempting to find the Work: {e.Message}");
        }

        if (booksWithAuthorData.Count == 0)
        {
            return null;
        }

        return new Book()
        {
            Id = workData.ID,
            Title = workData.Title,
            // Since we are searching by the OL ID, we can just use the first result since it is top ranked
            Authors = booksWithAuthorData.First().Authors,
            AuthorKeys = booksWithAuthorData.First().AuthorKeys,
            FirstPublishYear = booksWithAuthorData.First().FirstPublishYear,
            Description = workData.Description,
        };
    }
}
