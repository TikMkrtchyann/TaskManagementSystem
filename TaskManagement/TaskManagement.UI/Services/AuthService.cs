// --- START OF FILE Services/AuthService.cs (Cleaned) ---
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using TaskManagement.Shared.DTOs;
using TaskManagement.UI.Auth;

namespace TaskManagement.UI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginModel);
            if (!response.IsSuccessStatusCode) return new AuthResponseDto();

            var loginResult = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            if (loginResult == null || string.IsNullOrEmpty(loginResult.Token)) return new AuthResponseDto();

            await _localStorage.SetItemAsStringAsync("authToken", loginResult.Token);
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserAuthentication(loginResult.Token);

            // REMOVED the line that set DefaultRequestHeaders.Authorization

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).NotifyUserLogout();
            // REMOVED the line that set DefaultRequestHeaders.Authorization to null
        }
    }
}