using System.Net.Http.Json;
using System.Text.Json;
using TaskManagement.Shared.DTOs;
using TaskManagement.UI.Services.Interfaces;

namespace TaskManagement.UI.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskDto>> GetAllTasks()
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<List<TaskDto>>("api/task");
                return result;
            }
            catch
            {
                return new List<TaskDto>();
            }
        }

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

        public async Task<bool> DeleteTask(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/task/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}