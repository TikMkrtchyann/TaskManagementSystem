using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DAL.Entities;

namespace TaskManagement.DAL.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetByIdAsync(int id);
        Task<bool> UpdateAsync(TaskEntity task);
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<IEnumerable<TaskEntity>> GetAllAsync(int userId);
        Task<IEnumerable<AdminTaskEntity>> GetAllAdminTasks();
        Task<int> CreateAsync(TaskEntity task);
        Task<bool> DeleteAsync(int id);
    }
}
