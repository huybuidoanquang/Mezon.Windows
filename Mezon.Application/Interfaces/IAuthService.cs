using System.Threading.Tasks;

namespace Mezon.Application.Interfaces
{
    public interface IAuthService
    {
        bool IsLoggedIn { get; }

        Task<bool> LoginAsync(string username, string password);

        Task<bool> TryLoginWithSavedTokenAsync();

        Task LogoutAsync();
    }
}
