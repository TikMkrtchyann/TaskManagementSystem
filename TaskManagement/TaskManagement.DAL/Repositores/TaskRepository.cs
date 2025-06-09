using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TaskManagement.DAL.Entities;
using TaskManagement.DAL.Interfaces;

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

        public async Task<int> CreateAsync(TaskEntity task)
        {
            const string query = @"INSERT INTO Tasks (Title, Description, Status) OUTPUT INSERTED.Id VALUES (@title, @desc, @status)";

            using (var connection = new SqlConnection(_connectionString))
            { 
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@title", task.Title);
                    command.Parameters.AddWithValue("@desc", task.Description);
                    command.Parameters.AddWithValue("@status", task.Status);

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
            const string sql = "SELECT Id, Title, Description, Status FROM Tasks";
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
                    Status = reader.GetString(3)
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
                    Status = reader.GetString(3)
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
            cmd.Parameters.AddWithValue("@status", task.Status);
            cmd.Parameters.AddWithValue("@id", task.Id);

            await conn.OpenAsync();
            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }
}
