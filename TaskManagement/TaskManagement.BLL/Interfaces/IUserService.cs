using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.BLL.Interfaces
{
    public interface IUserService
    {
        Task<List<int>> GetAllUserId();
        Task<List<string>> GetAllUsernames();
    }
}
