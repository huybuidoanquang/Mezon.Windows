using Microsoft.UI.Xaml;

namespace Mezon.Application.Interfaces
{
    public enum AppTheme { Light, Dark, System }

    public interface IThemeService
    {
        ElementTheme CurrentTheme { get; }
        void SetTheme(ElementTheme theme);
        void ApplyThemeToWindow(Window window);
    }
}
