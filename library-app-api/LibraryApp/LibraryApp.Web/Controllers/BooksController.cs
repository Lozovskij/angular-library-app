using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;
    private readonly IMediator _mediator;

    public BooksController(
        IBooksService booksService,
        IMediator mediator)
    {
        _booksService = booksService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBooksQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook([FromQuery] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookQuery(id), cancellationToken);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("{bookId}/instance-status")]
    public async Task<ActionResult<BookInstanceStatus?>> GetBookInstanceStatus(
        [FromRoute] int bookId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetBookStatusQuery(bookId), cancellationToken);
        return Ok(result);
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
