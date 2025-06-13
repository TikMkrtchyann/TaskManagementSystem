using TaskManagement.Shared.DTOs;

namespace TaskManagement.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<int> CreateTaskAsync(CreateTaskDto dto, int userId);
        Task<int> CreateAdminTaskAsync(CreateAdminTaskDto dto);
        Task<List<TaskDto>> GetAllAsync();
        Task<List<TaskDto>> GetAllAsync(int userId);
        Task<List<AdminTaskDto>> GetAllAdminTasks();
        Task<TaskDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateTaskDto dto);
        Task<bool> UpdateStatusAsync(int id, UpdateTaskStatusDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
