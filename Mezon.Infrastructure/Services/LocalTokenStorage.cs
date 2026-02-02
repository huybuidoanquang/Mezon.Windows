using Mezon.Application.Interfaces;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mezon.Infrastructure.Services
{
    public class LocalTokenStorage : ITokenStorage
    {
        private const string TokenKey = "UserAuthToken";

        public Task SaveTokenAsync(string token)
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values[TokenKey] = token;
            return Task.CompletedTask;
        }

        public Task<string?> GetTokenAsync()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            if (localSettings.Values.TryGetValue(TokenKey, out var token))
            {
                return Task.FromResult(token as string);
            }
            return Task.FromResult<string?>(null);
        }

        public Task ClearTokenAsync()
        {
            var localSettings = ApplicationData.Current.LocalSettings;
            localSettings.Values.Remove(TokenKey);
            return Task.CompletedTask;
        }
    }
}