using Mezon.Application.Interfaces;
using Mezon.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Mezon.Presentation.Services
{
    public class WindowService : IWindowService
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ConcurrentDictionary<Type, Window> _activeWindows = new();

        public WindowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void OpenWindow<TWindow, TViewModel>()
            where TWindow : Window
            where TViewModel : IViewModelBase
        {
            OpenWindow<TWindow, TViewModel>(null);
        }

        public void OpenWindow<TWindow, TViewModel>(object parameter)
            where TWindow : Window
            where TViewModel : IViewModelBase
        {
            var window = _serviceProvider.GetRequiredService<TWindow>();
            var viewModel = _serviceProvider.GetRequiredService<TViewModel>();

            if (window.Content is FrameworkElement rootElement)
            {
                var themeService = _serviceProvider.GetService<IThemeService>();
                if (themeService != null)
                {
                    themeService.ApplyThemeToWindow(window);
                }
                rootElement.DataContext = viewModel;
            }

            viewModel.OnNavigatedTo(parameter);

            var windowType = typeof(TWindow);

            if (_activeWindows.TryGetValue(windowType, out var existingWindow))
            {
                existingWindow.Activate();
                return;
            }
            // Fallback trường hợp Window không có Content là FrameworkElement
            else
            {
                // WinUI 3 Window không có property DataContext trực tiếp, 
                // ta thường gán vào Root Grid trong XAML, hoặc dùng Extension Method.
                // Cách an toàn nhất là ép kiểu Content.
            }

            window.Closed += (s, e) =>
            {
                viewModel.OnClosed();
                _activeWindows.TryRemove(typeof(TViewModel), out _);
            };

            _activeWindows.TryAdd(typeof(TViewModel), window);
            window.Activate();
        }

        public void CloseWindow<TViewModel>() where TViewModel : IViewModelBase
        {
            if (_activeWindows.TryGetValue(typeof(TViewModel), out var window))
            {
                window.Close();
            }
        }

        public IEnumerable<Window> GetActiveWindows()
        {
            return _activeWindows.Values;
        }

        private void OnWindowClosed(Type windowType)
        {
            if (_activeWindows.TryRemove(windowType, out var window))
            {
                window.Closed -= (sender, args) => OnWindowClosed(windowType);
                window = null;
            }
        }
    }
}