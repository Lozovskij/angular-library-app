using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
    public async Task<ActionResult> CreateAndLoginDemoPatron()
    {
        var demoPatron = await _patronService.GenerateRandomPatronAsync();
        _patronRepository.Add(demoPatron);
        return Ok(_tokenService.Create(demoPatron));
    }

    //#if DEBUG
    [HttpGet("login-demo-patron/{id}")] //TODO should only be accessible on debug level
    public ActionResult<string> GetDemoToken([FromRoute] int id)
    {
        //if (id != 2 || id != 3) { throw new Exception(); }
        var patron = _patronRepository.GetById(id);
        return Ok(_tokenService.Create(patron));
    }
    //#endif
}
