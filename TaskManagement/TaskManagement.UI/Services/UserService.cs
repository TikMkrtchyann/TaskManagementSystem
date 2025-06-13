using System.Net.Http;
using System.Net.Http.Json;
using TaskManagement.UI.Services.Interfaces;

namespace TaskManagement.UI.Services
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
            var result = await _httpClient.GetFromJsonAsync<List<int>>("api/task/userIdList");
            if (result is null)
            {
                return new List<int>();
            }

            return result;
        }

        public async Task<List<string>> GetAllUsernames()
        {
            var result = await _httpClient.GetFromJsonAsync<List<string>>("api/task/usernames");
            if (result is null)
            {
                return new List<string>();
            }

            return result;
        }
    }
}
