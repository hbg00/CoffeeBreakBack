using CoffeeBreakAPI.Dtos.Auth;
using CoffeeBreakAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController
        (
            IAuthService authService
        )
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = await _authService.RegisterAsync(dto);
        if (user == null)
            return BadRequest("Invalid data or user already exists");

        return Ok(dto);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (result == null)
            return BadRequest(new { message = "Invalid email or password." });

        var response = new AuthResponseDto
        {
            Token = result.Value.Token,
            User = new UserDto
            {
                Email = result.Value.User.Email,
                FirstName = result.Value.User.FirstName,
                LastName = result.Value.User.LastName,
                PhoneNumber = result.Value.User.PhoneNumber ?? ""
            }
        };

        return Ok(response);
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto dto){
        var result = await _authService.LoginWithGoogleAsync(dto.IdToken);

        if (result == null)
            return BadRequest(new { message = "Invalid Google Token" });

        return Ok(new AuthResponseDto
        {
            Token = result.Value.Token,
            User = new UserDto
            {
                Email = result.Value.User.Email,
                FirstName = result.Value.User.FirstName,
                LastName = result.Value.User.LastName,
                PhoneNumber = result.Value.User.PhoneNumber
            }
        });

    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var user = await _authService.GetCurrentUserAsync(User);
        if (user == null) return Unauthorized();

        return Ok(user);
    }
}
