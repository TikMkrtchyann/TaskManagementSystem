using TaskManagement.Shared.DTOs;

namespace TaskManagement.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<int> RegisterAsync(RegisterDto dto);
    }
}
