using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.Interfaces;
using TaskManagement.DAL.Interfaces;
using TaskManagement.DAL.Repositores;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<int>> GetAllUserId()
        {
            var userIds = await _userRepository.GetAllUserId();
            return userIds.ToList();
        }

        public async Task<List<string>> GetAllUsernames()
        {
            var usernames = await _userRepository.GetAllUsernames();
            return usernames.ToList();
        }
    }
}
