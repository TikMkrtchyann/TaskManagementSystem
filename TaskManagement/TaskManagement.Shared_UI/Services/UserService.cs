using System.Net.Http;
using System.Net.Http.Json;
using TaskManagement.Shared.DTOs;
using TaskManagement.Shared_UI.Services.Interfaces;

namespace TaskManagement.Shared_UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> GetAllUserIds()
        {
            var result = await _httpClient.GetFromJsonAsync<List<int>>("api/user/userIdList");
            if (result is null)
            {
                return new List<int>();
            }

            return result;
        }

        public async Task<List<string>> GetAllUsernames()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("api/user/usernames");
            if (result is null)
            {
                return new List<string>();
            }

            return result;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var result = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/user");
            if (result is null)
            {
                return new List<UserDto>();
            }

            return result;
        }
    }
}
