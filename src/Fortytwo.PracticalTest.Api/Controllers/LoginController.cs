using Fortytwo.PracticalTest.Api.Auth;
using Fortytwo.PracticalTest.Api.Contracts.Login;
using Fortytwo.PracticalTest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Fortytwo.PracticalTest.Api.Controllers;

public class LoginController(
    JwtGenerator jwtGenerator) : ControllerBase
{
    private static readonly User Admin = new User{ UserName = "admin", Password = "admin123!" };
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest("Username and password required.");
        }


        if (Admin.UserName != request.Username && Admin.Password != request.Password)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }

        var token = jwtGenerator.GenerateJwtToken(Admin);
        return Ok(new { token });
    }
}