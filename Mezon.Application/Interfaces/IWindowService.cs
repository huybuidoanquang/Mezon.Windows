using Microsoft.UI.Xaml;
using System.Collections.Generic;

namespace Mezon.Application.Interfaces
{
    public interface IWindowService
    {
        void OpenWindow<TWindow, TViewModel>() where TWindow : Window where TViewModel : IViewModelBase;

        void OpenWindow<TWindow, TViewModel>(object parameter) where TWindow : Window where TViewModel : IViewModelBase;

        void CloseWindow<TViewModel>() where TViewModel : IViewModelBase;

        IEnumerable<Window> GetActiveWindows();
    }
}
