using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomRentalSystem.Application.DTOs;
using RoomRentalSystem.Application.Services.Interfaces;
using RoomRentalSystem.Persistence;

namespace RoomRentalSystem.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IUserService userService, IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto dto)
    {
        dto.RoleIds = new List<Guid>
        {
            Guid.Parse(DbSeeder.GuestId)
        };

        var user = await userService.CreateUserAsync(dto);

        return Ok(user);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var authResult = await authService.AuthenticateAsync(dto);

        var token = authResult.Token;

        Response.Cookies.Append("jwt", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true, 
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return Ok(authResult);
    }

    [HttpPost("logout")]
    [Authorize]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("jwt");
        return Ok();
    }
}