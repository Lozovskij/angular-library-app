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
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
    {
        var result = (await _booksRepository.GetAllAsync(cancellationToken))
            .Select(book => new BookDto(book));
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBook([FromQuery] int id, CancellationToken cancellationToken)
    {
        Book? book = await _booksRepository.GetByIdAsync(id, cancellationToken);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(new BookDto(book));
    }

    [HttpGet("{bookId}/instance-status")]
    public async Task<ActionResult<BookInstanceStatus?>> GetBookInstanceStatus(
        [FromRoute] int bookId,
        CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var instances = (await _bookInstancesRepository
            .GetWhereAsync(bi => bi.BookId == bookId, cancellationToken))
            .ToList();

        if (instances.Count == 0)
        {
            return Ok(null);// TODO (book details) better return 'not found' and handle appropriately on front-end (?)
        }
        //is this business logic?
        var status = instances.SingleOrDefault(i => i.PatronId == patronId)?.Status ??
            BookInstanceStatus.Available;

        return Ok(status);
    }

    [HttpPost("{bookId}/hold")]
    public async Task<ActionResult> Hold([FromQuery] int bookId, CancellationToken cancellationToken)
    {
        await _booksService.Hold(bookId, cancellationToken);
        return Ok();
    }

    [HttpPost("{bookId}/cancel-hold")]
    public async Task<ActionResult> CancelHold([FromQuery] int bookId, CancellationToken cancellationToken)
    {
        await _booksService.CancelHold(bookId, cancellationToken);
        return Ok();
    }
}
