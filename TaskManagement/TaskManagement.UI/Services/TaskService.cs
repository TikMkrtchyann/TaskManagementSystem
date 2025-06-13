using System.Net.Http.Json;
using System.Text.Json;
using TaskManagement.Shared.DTOs;
using TaskManagement.UI.Services.Interfaces;

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
            var result = await _httpClient.GetFromJsonAsync<List<TaskDto>>("api/task");
            if (result is null)
            {
                return new List<TaskDto>();
            }

            return result;
        }

        public async Task<List<AdminTaskDto>> GetAllAdminTasks()
        {
            var result = await _httpClient.GetFromJsonAsync<List<AdminTaskDto>>("api/task/admin");
            if (result is null)
            {
                return new List<AdminTaskDto>();
            }

            return result;
        }

        public async Task<TaskDto> GetTaskById(int id)
        {
           var result = await _httpClient.GetFromJsonAsync<TaskDto>($"api/task/{id}");
           if (result is null)
           {
                return new TaskDto();
           }

           return result;
        }
            
        public async Task<bool> CreateTask(CreateTaskDto task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/task", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateAdminTask(CreateAdminTaskDto task)
        {
            var response = await _httpClient.PostAsJsonAsync("api/task/admin", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTask(int id, UpdateTaskDto task)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/task/{id}", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskStatus(int id, UpdateTaskStatusDto task)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/task/{id}/status", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/task/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}