using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;

    public BooksController(
        IBooksRepository booksRepository
    )
    {
        _booksRepository = booksRepository;
    }

    [HttpGet]
    public IEnumerable<BookDto> GetBooks()
    {
        return _booksRepository.List().Select(book => new BookDto(book));
    }

    [HttpGet("{id}")]
    public BookDto GetBook([FromQuery] int id)
    {
        Book book = _booksRepository.GetById(id);
        return new BookDto(book);
    }
}
