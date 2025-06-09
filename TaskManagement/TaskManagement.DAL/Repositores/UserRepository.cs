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
            const string query = "SELECT Id, Username, PasswordHash, Role FROM Users WHERE Username = @username";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);

                    await connection.OpenAsync();
                    using var reader = await command.ExecuteReaderAsync();

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
            }
        }

        public async Task<int> CreateAsync(UserEntity user)
        {
            const string query = "INSERT INTO Users (Username, PasswordHash, Role) OUTPUT INSERTED.Id VALUES (@username, @hash, @role)";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", user.Username);
                    command.Parameters.AddWithValue("@hash", user.PasswordHash);
                    command.Parameters.AddWithValue("@role", user.Role);

                    await connection.OpenAsync();
                    return (int)await command.ExecuteScalarAsync();
                }
            }
        }
    }

}
