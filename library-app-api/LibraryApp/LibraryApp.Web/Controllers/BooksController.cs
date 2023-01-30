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

    public BooksController(
        IBooksRepository booksRepository,
        IBookInstancesRepository bookInstancesRepository)
    {
        _booksRepository = booksRepository;
        _bookInstancesRepository = bookInstancesRepository;
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
        //TODO move all logic to separate service
        var patronIdStr = HttpContext.User.Claims.First(c => c.Type == "userId").Value;
        var patronId = int.Parse(patronIdStr);
        var instances = _bookInstancesRepository.List(bi => bi.BookId == bookId).ToList();
        if (instances.Count == 0)
        {
            return null;
        }
        return instances.SingleOrDefault(i => i.PatronId == patronId)?.Status ?? BookInstanceStatus.Available;
    }

    [HttpPost("{bookId}/hold")]
    public ActionResult Hold([FromQuery] int bookId)
    {
        //do it in Core, bookInstanseService.Hold(bookId, patronId)
        //+ Get userId using infrastracture (userService)

        //var availableBookInstances = BookInstances.GetByBookId and by Status.Available (repository list query)
        //if availableBookInstances not null
        // bi = availableBookInstances.First()
        //bookInstanseService.Hold(bi.bookInstanceId, patronId);
        //set status onhold and patron id, save with repository, 
        return Ok();
    }
}
