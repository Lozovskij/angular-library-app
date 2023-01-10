using LibraryApp.Core.Entities;
using LibraryApp.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LibraryApp.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Create(Patron patron)
    {
        List<Claim> claims = new()
        {
            //TODO use constants
            new Claim(ClaimTypes.Role, "Patron"),
            new Claim("demoId", patron.DemoId.HasValue ? patron.DemoId.Value.ToString() : "")
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
            _configuration.GetSection("Token").Value ?? throw new Exception("Error. Can't find token key")));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),//TODO get this from somewhere, do not hardcode
            signingCredentials: creds);

        string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenStr;
    }
}
