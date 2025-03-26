using Application.Auth;
using Application.Auth.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebAPI.Controller;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : BaseController
{
    /// <summary>
    /// Logs a user in with email and password.
    /// </summary>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Email and password are required.");

        var token = await authService.Login(request.Email, request.Password);
        return ResponseOk(token);
    }

    /// <summary>
    /// Logs the current user out.
    /// </summary>
    /// <returns>A status indicating logout success.</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await authService.Logout();
        return ResponseOk();
    }

    /// <summary>
    /// Registers a new user with email and password.
    /// </summary>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            return BadRequest("Электронная почта и пароль обязательны.");
        
        var token = await authService.Register(request.Email, request.Password);
        return ResponseOk(token);
    }

    /// <summary>
    /// Registers or logs in a user using a Google token.
    /// </summary>
    [HttpPost("with-google")]
    [AllowAnonymous]
    public async Task<IActionResult> WithGoogle([FromBody] GoogleRegisterRequest request)
    {
        if (string.IsNullOrEmpty(request.Token))
            return ResponseFail("Google token is required.");
        
        var token = await authService.WithGoogle(request.Token);
        
        return ResponseOk(token);
    }
    
    [HttpGet("get-info")]
    [Authorize]
    public async Task<IActionResult> GetUserInfo(CancellationToken cancellationToken = default)
    {
        var user = await authService.GetInfo(cancellationToken);
        return ResponseOk(user);
    }
    
    [HttpPost("refresh-token/{refreshToken}")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromRoute] string refreshToken)
    {
        
        var token = await authService.RefreshToken(refreshToken);
        return ResponseOk(token);
    }
}