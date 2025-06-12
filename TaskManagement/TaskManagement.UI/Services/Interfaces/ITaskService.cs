using TaskManagement.Shared.DTOs;

namespace TaskManagement.UI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetAllTasks();
        Task<TaskDto> GetTaskById(int id);
        Task<bool> CreateTask(CreateTaskDto task);
        Task<bool> UpdateTask(int id, UpdateTaskDto task);
        Task<bool> DeleteTask(int id);
    }
}