using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using LibraryApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryApp.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DemoController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IPatronService _patronService;
    private readonly IRepository<Patron> _patronRepository;

    public DemoController(
        ITokenService tokenService,
        IPatronService patronService,
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
}
