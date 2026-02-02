using Mezon.Application.Interfaces;
using Mezon.Infrastructure.Services;
using Mezon.Presentation.Services;
using Mezon.Presentation.ViewModels;
using Mezon.Presentation.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Mezon.Presentation
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Microsoft.UI.Xaml.Application
    {
        public new static App Current => (App)Microsoft.UI.Xaml.Application.Current;

        public IServiceProvider Services { get; }

        private Window? _window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();

            Services = ConfigureServices();
        }

        /// <summary>
        /// Nơi đăng ký tất cả các Class/Interface của dự án (The Composition Root)
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // =========================================================
            // A. LAYER INFRASTRUCTURE (HẠ TẦNG)
            // =========================================================

            // 1. Đăng ký gRPC Channel (Singleton - Chỉ tạo 1 kết nối duy nhất)
            // services.AddSingleton(sp =>
            // {
            //     // Lưu ý: Trong môi trường Dev (localhost), bạn có thể cần cấu hình để bỏ qua lỗi SSL
            //     // return GrpcChannel.ForAddress("https://localhost:50051");
            // });
            // 2. Đăng ký Repository (Mapping Interface -> Implementation)
            // Khi ai đó xin IChatRepository, đưa cho họ GrpcChatRepository
            // services.AddSingleton<IChatRepository, GrpcChatRepository>();


            // =========================================================
            // B. LAYER APPLICATION (USE CASES)
            // =========================================================

            // Đăng ký các UseCase (Transient - Tạo mới mỗi khi cần)
            // services.AddTransient<SendMessageUseCase>();
            // services.AddTransient<GetMessageStreamUseCase>();


            // =========================================================
            // C. LAYER PRESENTATION (UI)
            // =========================================================

            // 1. Đăng ký ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();

            // 2. Đăng ký MainWindow (Để có thể Inject ViewModel vào Constructor của Window)
            services.AddSingleton<IWindowService, WindowService>();
            services.AddSingleton<IThemeService, ThemeService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<ITokenStorage, LocalTokenStorage>();

            services.AddTransient<MainWindow>();
            services.AddTransient<LoginWindow>();

            return services.BuildServiceProvider();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var windowService = Services.GetRequiredService<IWindowService>();
            var authService = Services.GetRequiredService<IAuthService>();

            // Logic kiểm tra đăng nhập
            // Lưu ý: Nên hiện một Splash Screen nhỏ trong lúc check token nếu mất thời gian
            bool isAuthenticated = await authService.TryLoginWithSavedTokenAsync();

            if (isAuthenticated)
            {
                windowService.OpenWindow<MainWindow, MainViewModel>();
            }
            else
            {
                windowService.OpenWindow<LoginWindow, LoginViewModel>();
            }
        }
    }
}
