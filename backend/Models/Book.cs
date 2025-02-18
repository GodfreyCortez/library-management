namespace book_library_manager.Models;

/**
    Book model which contains all the details about a single book within the
    database.
*/
public class Book
{
    /**
    Maps directly to the OpenLibrary key of 
    a Work
    */
    public required string Id { get; set; }
    public string? Title { get; set; }
    public List<string>? Authors { get; set; }
    public List<string>? AuthorKeys { get; set; }
    public string? FirstPublishYear { get; set; }
    public string? Description { get; set; }
    public string? CoverId { get; set; }

}