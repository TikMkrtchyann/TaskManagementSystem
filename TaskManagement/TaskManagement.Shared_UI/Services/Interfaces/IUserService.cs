using TaskManagement.Shared.DTOs;

namespace TaskManagement.Shared_UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<int>> GetAllUserIds();
        Task<List<string>> GetAllUsernames();
        Task<List<UserDto>> GetAllUsers();
    }
}
