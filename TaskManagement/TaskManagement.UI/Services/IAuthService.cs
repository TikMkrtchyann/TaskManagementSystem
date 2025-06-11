using TaskManagement.Shared.DTOs;

namespace TaskManagement.UI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginModel);
        Task Logout();
    }
}