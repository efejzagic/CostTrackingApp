using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthenticationManager.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("not-before-policy")]
        public int NotBeforePolicy { get; set; }

        [JsonProperty("session_state")]
        public string SessionState { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("roles")]  // Change this property Name if needed
        public List<string> Roles { get; set; }
    }
}
