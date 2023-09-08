using Microsoft.Extensions.Caching.Memory;

namespace Auth.WebAPI.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {

        private readonly IMemoryCache _cache;
        private readonly TimeSpan _tokenExpiration = TimeSpan.FromMinutes(15); // Set the token expiration time

        public TokenBlacklistService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddToBlacklist(string token)
        {
            // Add the token to the blacklist with an expiration time
            _cache.Set(token, true, _tokenExpiration);
        }

        public bool IsTokenBlacklisted(string token)
        {
            // Check if the token exists in the blacklist
            return _cache.TryGetValue(token, out _);
        }

    }
}
