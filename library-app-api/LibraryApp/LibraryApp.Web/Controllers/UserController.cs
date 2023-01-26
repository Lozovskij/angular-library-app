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

    public UserController(
        IRepository<Patron> patrons, 
        IBookInstancesRepository bookInstancesRepository)
    {
        _patrons = patrons;
        _bookInstancesRepository = bookInstancesRepository;
    }

    [HttpGet("patron-profile")]
    public PatronProfileDto GetCurrentPatron()
    {
        //TODO create constants for claim keys
        var patronId = HttpContext.User.Claims.First(c => c.Type == "userId").Value;
        var patron = _patrons.GetById(int.Parse(patronId));
        var bookInstances = _bookInstancesRepository.GetBookInstancesByPatronId(int.Parse(patronId));
        return new PatronProfileDto(patron, bookInstances.ToList());
    }
}
