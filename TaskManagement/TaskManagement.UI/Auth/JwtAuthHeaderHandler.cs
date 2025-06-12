// --- START OF FILE Auth/JwtAuthHeaderHandler.cs ---
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TaskManagement.UI.Auth
{
    public class JwtAuthHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public JwtAuthHeaderHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get the token from local storage on EVERY request
            var token = await _localStorage.GetItemAsStringAsync("authToken");

            // If the token exists, add it to the Authorization header
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Continue sending the request
            return await base.SendAsync(request, cancellationToken);
        }
    }
}