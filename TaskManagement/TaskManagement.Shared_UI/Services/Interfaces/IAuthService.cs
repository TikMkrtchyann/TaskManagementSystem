using TaskManagement.Shared.DTOs;

namespace TaskManagement.Shared_UI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginModel);
        Task<bool> Register(RegisterDto registerModel);
        Task Logout();
    }
}