
using book_library_manager.Services;
using Microsoft.AspNetCore.Mvc;
using book_library_manager.Models;

namespace book_library_manager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;

    public BooksController(BooksService booksService)
    {
        _booksService = booksService;
    }

    [HttpGet]
    public async Task<List<Book>> Get()
    {
        return await _booksService.GetBooks();
    }
}