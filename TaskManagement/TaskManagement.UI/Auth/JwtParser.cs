// --- START OF FILE Auth/JwtParser.cs (Final Corrected Version) ---
using System.Security.Claims;
using System.Text.Json;

namespace TaskManagement.UI.Auth
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                // --- THIS IS THE CRITICAL LOGIC ---
                // 1. Specifically find the 'role' claim.
                keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

                if (roles != null)
                {
                    // 2. Handle if the role is a single string or an array of strings.
                    if (roles.ToString().Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());
                        foreach (var parsedRole in parsedRoles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                    }
                    // 3. Remove the role from the dictionary so it's not added twice.
                    keyValuePairs.Remove(ClaimTypes.Role);
                }
                // --- END OF CRITICAL LOGIC ---

                // Add all other claims (like name, email, etc.)
                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
            }
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}