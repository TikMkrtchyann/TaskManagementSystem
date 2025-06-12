using System.Security.Cryptography;
using System.Text;
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

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
                return null;

            var hash = HashPassword(dto.Password);
            if (user.PasswordHash != hash)
                return null;

            var userRole = string.IsNullOrEmpty(user.Role) ? "User" : user.Role;

            return new AuthResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
            };
        }

        public async Task<int> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return 0;
            }

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