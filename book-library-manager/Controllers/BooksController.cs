
using book_library_manager.Services;
using Microsoft.AspNetCore.Mvc;
using book_library_manager.Models;
using OpenLibraryNET.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace book_library_manager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;
    private readonly OpenLibraryService _olService;

    public BooksController(BooksService booksService, OpenLibraryService olService)
    {
        _booksService = booksService;
        _olService = olService;
    }

    [HttpGet]
    public async Task<List<Book>> Get()
    {
        return await _booksService.GetBooks();
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<Book>>> SearchBooks([FromQuery] string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            return BadRequest("Query parameter is required");
        }

        var searchResults = await _olService.SearchWorks(query);

        return searchResults;
    }

    /**
    Given a OpenLibrary ID, we will 
    add a book to the database

    In the future, we will want to support including cover IDs.
    This will query the OpenLibrary API for an image which will allow for us to 
    end up displaying the cover images of the books the user has in their library
    */
    [HttpPost]
    public async Task<IActionResult> Post(BookIdentifier bookIdentifier)
    {
        var newBook = await _olService.GetBook(bookIdentifier);

        if (newBook == null)
        {
            return UnprocessableEntity();
        }

        await _booksService.CreateBook(newBook);

        return CreatedAtAction(nameof(Get), new { Id = newBook.Id });
    }

    [HttpDelete("{id}")]
    public async Task Delete(string id)
    {
        var book = new BookIdentifier()
        {
            Id = id
        };

        await _booksService.DeleteBook(book);
    }
}