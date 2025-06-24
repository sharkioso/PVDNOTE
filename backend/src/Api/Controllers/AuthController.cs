using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using PVDNOTE.Backend.Api.DTO;
using PVDNOTE.Backend.Core.Entities;
using PVDNOTE.Backend.Infrastructure.Data;

// бизнес логику надо чуть позже вынести из контроллеров
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly DBContext context;
    private readonly AuthService authService;

    public AuthController(DBContext context)
    {
        this.context = context;
        authService = new AuthService(context);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        try
        {
            var user =  await authService.RegisterService(dto);
            return Ok(new { user.Id });
        }
        catch (ApplicationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            var user = await authService.LoginService(dto);
            return Ok(new { userId = user.Id });
        }
        catch (ApplicationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

}