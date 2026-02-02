using Mezon.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Mezon.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenStorage _tokenStorage;

        // Trạng thái lưu trong RAM
        public bool IsLoggedIn { get; private set; } = false;

        public AuthService(ITokenStorage tokenStorage)
        {
            _tokenStorage = tokenStorage;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            try
            {
                // 1. Gọi thư viện Mezon.NET để lấy token từ Server
                // Giả sử thư viện của bạn có hàm này và trả về Token string
                string token = "123";

                if (!string.IsNullOrEmpty(token))
                {
                    // 2. Lưu token xuống ổ cứng
                    await _tokenStorage.SaveTokenAsync(token);

                    // 3. Cập nhật trạng thái client
                    IsLoggedIn = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log lỗi tại đây (Serilog)
            }
            return false;
        }

        public async Task<bool> TryLoginWithSavedTokenAsync()
        {
            // 1. Lấy token từ storage
            var token = await _tokenStorage.GetTokenAsync();

            if (string.IsNullOrEmpty(token))
            {
                IsLoggedIn = false;
                return false;
            }

            try
            {
                // 2. Thử kết nối với Token đó
                // Giả sử thư viện Mezon.NET có hàm Connect hoặc Verify Token
                IsLoggedIn = true;
                return true;
            }
            catch
            {
                // Token hết hạn hoặc lỗi mạng -> Xóa token để bắt đăng nhập lại
                await _tokenStorage.ClearTokenAsync();
                IsLoggedIn = false;
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            await _tokenStorage.ClearTokenAsync();
            // _mezonClient.Disconnect(); // Nếu cần
            IsLoggedIn = false;
        }
    }
}