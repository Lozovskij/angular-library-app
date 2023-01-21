using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRepository<Patron> _patrons;

    public UserController(IRepository<Patron> patrons)
    {
        _patrons = patrons;
    }

    [HttpGet("patron-profile")]
    public PatronProfileDto GetCurrentPatron()
    {
        //TODO create constants for claim keys
        var patronId = HttpContext.User.Claims.First(c => c.Type == "userId").Value;
        var patron = _patrons.GetById(int.Parse(patronId));
        return new PatronProfileDto(patron);
    }
}
