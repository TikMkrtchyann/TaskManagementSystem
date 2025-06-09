using TaskManagement.BLL.Interfaces;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;
using TaskManagement.Shared.DTOs;
using TaskManagement.Shared.Enums;

namespace TaskManagement.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateTaskAsync(CreateTaskDto dto)
        {
            var entity = new TaskEntity
            {
                Title = dto.Title,
                Description = dto.Description,
                Status = Shared.Enums.TaskStatus.NotStarted,
            };

            return await _repository.CreateAsync(entity);
        }

        public async Task<List<TaskDto>> GetAllAsync()
        {
            var tasks = await _repository.GetAllAsync();

            return tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status
            }).ToList();
        }

        public async Task<TaskDto?> GetByIdAsync(int id)
        {
            var task = await _repository.GetByIdAsync(id);
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

            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
