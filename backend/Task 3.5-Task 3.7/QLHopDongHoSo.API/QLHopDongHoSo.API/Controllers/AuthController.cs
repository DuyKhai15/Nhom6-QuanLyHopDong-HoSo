using Microsoft.AspNetCore.Mvc;
using QLHopDongHoSo.API.DTOs;
using QLHopDongHoSo.API.Services;

namespace QLHopDongHoSo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new
            {
                message = "Username và password không được để trống."
            });
        }

        var result = await _authService.LoginAsync(request);

        if (result == null)
        {
            return Unauthorized(new
            {
                message = "Username hoặc password không đúng."
            });
        }

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password) ||
            string.IsNullOrWhiteSpace(request.FullName))
        {
            return BadRequest(new
            {
                message = "Username, Password và FullName không được để trống."
            });
        }

        var result = await _authService.RegisterAsync(request);

        if (result == null)
        {
            return BadRequest(new
            {
                message = "Username đã tồn tại."
            });
        }

        return Ok(result);
    }
}