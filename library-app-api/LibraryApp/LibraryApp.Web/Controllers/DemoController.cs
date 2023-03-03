using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IRandomPatronGenerator _patronService;
    private readonly IRepository<Patron> _patronRepository;

    public DemoController(
        ITokenService tokenService,
        IRandomPatronGenerator patronService,
        IRepository<Patron> patronRepository)
    {
        _tokenService = tokenService;
        _patronService = patronService;
        _patronRepository = patronRepository;
    }

    [HttpPost("create-and-login-demo-patron")]
    public async Task<ActionResult> CreateAndLoginDemoPatron(CancellationToken cancellationToken)
    {
        var demoPatron = await _patronService.GenerateRandomPatronAsync();
        await _patronRepository.AddAsync(demoPatron, cancellationToken);
        return Ok(_tokenService.Create(demoPatron));
    }

    //#if DEBUG
    [HttpGet("login-demo-patron/{id}")] //TODO should only be accessible on debug level
    public async Task<ActionResult<string>> GetDemoToken([FromRoute] int id, CancellationToken cancellationToken)
    {
        //if (id != 2 || id != 3) { throw new Exception(); }
        var patron = await _patronRepository.GetByIdAsync(id, cancellationToken);
        if (patron == null) { return NotFound(); }
        return Ok(_tokenService.Create(patron));
    }
    //#endif
}
