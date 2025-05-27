using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PVDNOTE.Backend.Api.DTO;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

// бизнес логику надо чуть позже вынести из контроллеров
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DBContext context;

    public AuthController(DBContext Context)
    {
        context = Context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        if (await context.Users.AnyAsync(u => u.Login == dto.Login))
            return BadRequest(new { message = "Email уже зарегистрирован " });

        var user = new User
        {
            Login = dto.Login,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return Ok(new { user.Id });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == dto.Login);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            return Unauthorized(new { message = "неверный пароль или email" });
        return Ok(new
        {
            userId = user.Id,
        });
    }

}