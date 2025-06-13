using TaskManagement.BLL.Interfaces;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;
using TaskManagement.DAL.Repositores;
using TaskManagement.Shared.DTOs;
using TaskManagement.Shared.Enums;

namespace TaskManagement.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<int> CreateTaskAsync(CreateTaskDto dto, int userId)
        {
            var entity = new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = Shared.Enums.TaskStatus.NotStarted,
                UserId = userId
            };

            return await _taskRepository.CreateAsync(entity);
        }

        public async Task<int> CreateAdminTaskAsync(CreateAdminTaskDto dto)
        {
            var entity = new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = Shared.Enums.TaskStatus.NotStarted,
                UserId = dto.UserData.Keys.First(),
            };

            return await _taskRepository.CreateAsync(entity);
        }

        public async Task<List<TaskDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                UserId = t.UserId
            }).ToList();
        }

        public async Task<List<TaskDto>> GetAllAsync(int userId)
        {
            var tasks = await _taskRepository.GetAllAsync(userId);

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                UserId = userId
            }).ToList();
        }

        public async Task<List<AdminTaskDto>> GetAllAdminTasks()
        {
            var tasks = await _taskRepository.GetAllAdminTasks();

            return tasks.Select(t => new AdminTaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                Username = t.Username,
            }).ToList();

        }

        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return null;

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto)
        {
            var entity = new TaskEntity
            {
                Id = id,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
            };

            return await _taskRepository.UpdateAsync(entity);
        }

        public async Task<bool> UpdateStatusAsync(int id, UpdateTaskStatusDto dto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null)
            {
                return false;
            }

            existingTask.Status = dto.Status;

            return await _taskRepository.UpdateAsync(existingTask);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }

    }
}
