using LibraryApp.Core.Entities;
using LibraryApp.Core.Handlers.Commands;
using LibraryApp.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
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
        await _mediator.Send(new HoldBookCommand(bookId), cancellationToken);
        return Ok();
    }

    [HttpPost("{bookId}/cancel-hold")]
    public async Task<ActionResult> CancelHold([FromQuery] int bookId, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CancelHoldCommand(bookId), cancellationToken);
        return Ok();
    }
}
