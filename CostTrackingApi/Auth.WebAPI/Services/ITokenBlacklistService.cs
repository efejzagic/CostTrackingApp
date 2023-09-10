using Microsoft.Extensions.Caching.Memory;

namespace Auth.WebAPI.Services
{
    public interface ITokenBlacklistService
    {

        void AddToBlacklist(string token);

        bool IsTokenBlacklisted(string token);
    }
}
