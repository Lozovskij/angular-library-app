using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Models;
using LibraryApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly IRepository<BookInstance> _instancesRepository;

    public BooksController(
        IBooksRepository booksRepository,
        IRepository<BookInstance> instancesRepository
    )
    {
        _booksRepository = booksRepository;
        _instancesRepository = instancesRepository;
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

    [HttpGet("{id}/book-instances")]
    public IEnumerable<BookInstanceDto> GetBookInstances([FromQuery] int id)
    {
        return _instancesRepository.List(i => i.BookId == id).Select(bi => new BookInstanceDto(bi));
    }
}
