using Mezon.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;

namespace Mezon.Presentation.Services
{
    public class ThemeService : IThemeService
    {
        // Sử dụng Lazy hoặc func để tránh Circular Dependency trong Constructor
        // Vì WindowService gọi ThemeService, và ThemeService gọi WindowService
        private readonly IServiceProvider _serviceProvider;

        public ElementTheme CurrentTheme { get; private set; } = ElementTheme.Dark;

        public ThemeService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SetTheme(ElementTheme theme)
        {
            CurrentTheme = theme;

            // 1. Lấy WindowService để tìm tất cả cửa sổ đang mở
            var windowService = _serviceProvider.GetRequiredService<IWindowService>();

            // 2. Duyệt qua từng cửa sổ và đổi theme
            foreach (var window in windowService.GetActiveWindows())
            {
                ApplyThemeToWindow(window);
            }

            // 3. (Optional) Lưu vào LocalSettings để lần sau mở lại nhớ theme này
            // _settingsService.Save("AppTheme", theme.ToString());
        }

        // Hàm helper để áp dụng theme cho 1 cửa sổ bất kỳ
        public void ApplyThemeToWindow(Window window)
        {
            if (window.Content is FrameworkElement root)
            {
                root.RequestedTheme = CurrentTheme;
            }
        }
    }
}