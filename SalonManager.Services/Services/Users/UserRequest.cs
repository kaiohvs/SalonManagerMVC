using Newtonsoft.Json;

namespace SalonManager.Services.Services.Users
{
    public class UserRequest
    {
        [JsonProperty("username")]
        public string? Username { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("passwordHash")]
        public string? PasswordHash { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }
    }
}
