namespace book_library_manager.Models;

/**
    Book model which contains all the details about a single book within the
    database.
*/
public class Book {
    public required string Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
}