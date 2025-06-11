// --- START OF FILE TaskService.cs (Updated) ---
using System.Net.Http.Json;
using TaskManagement.Shared.DTOs;

namespace TaskManagement.UI.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskDto>> GetAllTasks()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<TaskDto>>("api/task");
            }
            catch
            {
                return new List<TaskDto>();
            }
        }

        // --- ADD THE FOLLOWING METHOD ---
        public async Task<TaskDto> GetTaskById(int id)
        {
            return await _httpClient.GetFromJsonAsync<TaskDto>($"api/task/{id}");
        }

        public async Task<bool> CreateTask(CreateTaskDto task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/task", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTask(int id, UpdateTaskDto task)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/task/{id}", task);
            return response.IsSuccessStatusCode;
        }
    }
}