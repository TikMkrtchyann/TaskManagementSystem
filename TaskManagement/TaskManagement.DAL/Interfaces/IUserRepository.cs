using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DAL.Entities;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByUsernameAsync(string username);
        Task<IEnumerable<int>> GetAllUserId();
        Task<IEnumerable<string>> GetAllUsernames();
        Task<int> CreateAsync(UserEntity user);
        Task<IEnumerable<UserEntity>> GetAllAsync();
    }

}
