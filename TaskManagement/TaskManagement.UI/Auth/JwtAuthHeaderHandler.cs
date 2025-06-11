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
            var token = await _localStorage.GetItemAsStringAsync("authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}