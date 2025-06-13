using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;
using TaskStatus = TaskManagement.Shared.Enums.TaskStatus;

// change naming
namespace TaskManagement.DAL.Repositores
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<IEnumerable<AdminTaskEntity>> GetAllAdminTasks()
        {
            var adminTasks = new List<AdminTaskEntity>();

            const string query = "SELECT t.Id, t.Title, t.Description, t. Status, u.Username FROM Tasks t JOIN Users u ON t.UserId = u.Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();
                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        adminTasks.Add(new AdminTaskEntity
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Status = Enum.Parse<TaskStatus>(reader.GetString(3)),
                            Username = reader.GetString(4),
                        });
                    }
                }
            }

            return adminTasks;
        }

        public async Task<int> CreateAsync(TaskEntity task)
        {
            const string query = @"INSERT INTO Tasks (Title, Description, Status, UserId) OUTPUT INSERTED.Id VALUES (@title, @desc, @status, @userId)";

            using (var connection = new SqlConnection(_connectionString))
            { 
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", task.Title);
                    command.Parameters.AddWithValue("@desc", task.Description);
                    command.Parameters.AddWithValue("@status", task.Status.ToString());
                    command.Parameters.AddWithValue("@userId", task.UserId);

                    await connection.OpenAsync();
                    return (int)await command.ExecuteScalarAsync();
                }
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Tasks WHERE Id = @id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            const string sql = "SELECT Id, Title, Description, Status, UserId FROM Tasks";
            var tasks = new List<TaskEntity>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tasks.Add(new TaskEntity
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Status = Enum.Parse<TaskStatus>(reader.GetString(3)),
                    UserId = reader.GetInt32(4),
                });
            }

            return tasks;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync(int userId)
        {
            const string sql = "SELECT Id, Title, Description, Status, UserId FROM Tasks WHERE UserId = @userId";
            var tasks = new List<TaskEntity>();

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tasks.Add(new TaskEntity
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Status = Enum.Parse<TaskStatus>(reader.GetString(3))
                });
            }

            return tasks;
        }

        public async Task<TaskEntity> GetByIdAsync(int id)
        {
            const string sql = "SELECT Id, Title, Description, Status FROM Tasks WHERE Id = @id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new TaskEntity
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.GetString(2),
                    Status = Enum.Parse<TaskStatus>(reader.GetString(3))
                };
            }

            return null;
        }

        public async Task<bool> UpdateAsync(TaskEntity task)
        {
            const string sql = "UPDATE Tasks SET Title = @title, Description = @desc, Status = @status WHERE Id = @id";
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@title", task.Title);
            cmd.Parameters.AddWithValue("@desc", task.Description);
            cmd.Parameters.AddWithValue("@status", task.Status.ToString());
            cmd.Parameters.AddWithValue("@id", task.Id);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
