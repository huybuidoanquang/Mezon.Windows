using System.Threading.Tasks;

namespace Mezon.Application.Interfaces
{
    public interface ITokenStorage
    {
        Task SaveTokenAsync(string token);
        Task<string?> GetTokenAsync();
        Task ClearTokenAsync();
    }
}
