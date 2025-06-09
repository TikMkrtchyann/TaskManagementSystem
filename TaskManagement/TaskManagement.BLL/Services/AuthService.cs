using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BLL.Interfaces;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
                return null;

            var hash = HashPassword(dto.Password);
            if (user.PasswordHash != hash)
                return null;

            return new AuthResponseDto
            {
                Username = dto.Username,
                Role = user.Role,
            };
        }

        public async Task<int> RegisterAsync(RegisterDto dto)
        {
            var hash = HashPassword(dto.Password);
            var user = new UserEntity
            {
                Username = dto.Username,
                PasswordHash = hash,
                Role = dto.Role,
            };

            return await _userRepository.CreateAsync(user);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(sha256.ComputeHash(bytes));
            }
        }
    }
}
