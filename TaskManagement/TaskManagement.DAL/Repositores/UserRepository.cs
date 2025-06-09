using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;

// change naming
namespace TaskManagement.DAL.Repositores
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public async Task<UserEntity?> GetByUsernameAsync(string username)
        {
            const string sql = "SELECT Id, Username, PasswordHash, Role FROM Users WHERE Username = @username";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);

            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new UserEntity
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                    Role = reader.GetString(3)
                };
            }

            return null;
        }

        public async Task<int> CreateAsync(UserEntity user)
        {
            const string sql = "INSERT INTO Users (Username, PasswordHash, Role) OUTPUT INSERTED.Id VALUES (@username, @hash, @role)";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", user.Username);
            cmd.Parameters.AddWithValue("@hash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@role", user.Role);

            await conn.OpenAsync();
            return (int)await cmd.ExecuteScalarAsync();
        }
    }

}
