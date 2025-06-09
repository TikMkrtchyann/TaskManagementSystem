using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.API.Services;
using TaskManagement.BLL.Interfaces;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;
        private readonly IAuthService _authService;

        public AuthController(JwtTokenService jwtTokenService, IAuthService authService)
        {
            _jwtTokenService = jwtTokenService;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized();

            result.Token = _jwtTokenService.GenerateToken(result.Username, result.Role);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var userId = await _authService.RegisterAsync(dto);
            return Ok(new { id = userId });
        }
    }
}
