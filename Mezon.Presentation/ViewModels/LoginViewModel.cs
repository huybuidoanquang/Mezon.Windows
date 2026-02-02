using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mezon.Application.Interfaces;
using Mezon.Presentation;
using Mezon.Presentation.ViewModels;
using System.Threading.Tasks;

public partial class LoginViewModel : ViewModelBase
{
    private readonly IAuthService _authService;
    private readonly IWindowService _windowService;

    [ObservableProperty] private string _username;
    [ObservableProperty] private string _password;
    [ObservableProperty] private string _errorMessage;

    public LoginViewModel(IAuthService authService, IWindowService windowService)
    {
        _authService = authService;
        _windowService = windowService;
    }

    [RelayCommand]
    public async Task Login()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Vui lòng nhập đầy đủ thông tin";
            return;
        }

        // Gọi AuthService
        bool success = await _authService.LoginAsync(Username, Password);

        if (success)
        {
            // Login thành công -> Chuyển màn hình
            _windowService.OpenWindow<MainWindow, MainViewModel>();
            _windowService.CloseWindow<LoginViewModel>();
        }
        else
        {
            ErrorMessage = "Sai tài khoản hoặc mật khẩu!";
        }
    }
}