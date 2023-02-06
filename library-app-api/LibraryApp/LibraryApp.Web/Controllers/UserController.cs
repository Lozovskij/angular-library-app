using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Core.Interfaces.Repositories;
using LibraryApp.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRepository<Patron> _patrons;
    private readonly IBookInstancesRepository _bookInstancesRepository;
    private readonly IUserService _userService;

    public UserController(
        IRepository<Patron> patrons,
        IBookInstancesRepository bookInstancesRepository,
        IUserService userService)
    {
        _patrons = patrons;
        _bookInstancesRepository = bookInstancesRepository;
        _userService = userService;
    }

    [HttpGet("patron-profile")]
    public async Task<ActionResult<PatronProfileDto>> GetCurrentPatron(CancellationToken cancellationToken)
    {
        var patronId = _userService.GetUserId();
        var patron = await _patrons.GetByIdAsync(patronId, cancellationToken);
        if (patron == null) { return NotFound(); } //TODO move to separate service
        var bookInstances = await _bookInstancesRepository
            .GetWhereAsync(bi => bi.PatronId == patronId, cancellationToken);
        return new PatronProfileDto(patron, bookInstances.ToList());
    }
}
