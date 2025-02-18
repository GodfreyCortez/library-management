
/**
  Class which determines the expected body for
  the BooksController Post endpoint, contains the necessary information
  to generate 
*/
public class BookIdentifier
{
    /**
    Maps directly to the OpenLibrary key of 
    a Work
    */
    public required string Id { get; set; }

}