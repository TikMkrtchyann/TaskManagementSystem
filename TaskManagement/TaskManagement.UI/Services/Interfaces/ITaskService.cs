using TaskManagement.Shared.DTOs;

namespace TaskManagement.UI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasks();
        Task<List<AdminTaskDto>> GetAllAdminTasks();
        Task<TaskDto> GetTaskById(int id);
        Task<bool> CreateTask(CreateTaskDto task);
        Task<bool> CreateAdminTask(CreateAdminTaskDto task);
        Task<bool> UpdateTask(int id, UpdateTaskDto task);
        Task<bool> UpdateTaskStatus(int id, UpdateTaskStatusDto task);
        Task<bool> DeleteTask(int id);
    }
}