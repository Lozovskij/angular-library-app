using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Data.Repositories;
using LibraryApp.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    private readonly IBookInstancesRepository _bookInstancesRepository;
    private readonly IUserService _userService;
    private readonly IBooksService _booksService;


    public BooksController(
        IBooksRepository booksRepository,
        IBookInstancesRepository bookInstancesRepository,
        IUserService userService,
        IBooksService booksService)
    {
        _booksRepository = booksRepository;
        _bookInstancesRepository = bookInstancesRepository;
        _userService = userService;
        _booksService = booksService;
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

    [HttpGet("{bookId}/instance-status")]
    public BookInstanceStatus? GetBookInstanceStatus([FromRoute] int bookId, CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var instances = _bookInstancesRepository.List(bi => bi.BookId == bookId).ToList();
        if (instances.Count == 0)
        {
            return null;
        }
        return instances.SingleOrDefault(i => i.PatronId == patronId)?.Status ?? BookInstanceStatus.Available;
    }

    [HttpPost("{bookId}/hold")]
    public async Task<ActionResult> Hold([FromQuery] int bookId)
    {
        await _booksService.Hold(bookId);
        return Ok();
    }

    [HttpPost("{bookId}/cancel-hold")]
    public async Task<ActionResult> CancelHold([FromQuery] int bookId)
    {
        await _booksService.CancelHold(bookId);
        return Ok();
    }
}
