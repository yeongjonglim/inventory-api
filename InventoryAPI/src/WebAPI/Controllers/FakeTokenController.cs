using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using InventoryAPI.Infrastructure;
using InventoryAPI.Infrastructure.Persistence;

namespace InventoryAPI.WebAPI.Controllers;

public class FakeTokenController : ApiControllerBase
{
    private readonly JwtConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public FakeTokenController(JwtConfiguration config, ApplicationDbContext context)
    {
        _configuration = config;
        _context = context;
    }

    [HttpPost]
    public IActionResult Post()
    {
        var user = GetUser();

        //create claims details based on the user information
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _configuration.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            new Claim("UserId", user.UserId), new Claim("DisplayName", user.DisplayName),
            new Claim("UserName", user.UserName), new Claim("Email", user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _configuration.Issuer,
            _configuration.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: signIn);

        return Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }

    private UserInfo GetUser()
    {
        return new UserInfo
        {
            Email = "abcde@example.com",
            DisplayName = "abcde",
            UserName = "Hello abcde",
            UserId = Guid.NewGuid().ToString()
        };
    }
}

public class UserInfo
{
    public string UserId { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
}