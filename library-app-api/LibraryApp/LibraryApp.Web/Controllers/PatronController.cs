using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PatronController : ControllerBase
{
    private readonly IMediator _mediator;

    public PatronController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetPatronQueryResult>> GetPatron(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPatronQuery(), cancellationToken);
    }

    [HttpGet("books")]
    public async Task<ActionResult<GetPatronBooksQueryResult>> GetPatronBooks(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetPatronBooksQuery(), cancellationToken);
    }
}
