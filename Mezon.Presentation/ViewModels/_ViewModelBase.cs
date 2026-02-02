using CommunityToolkit.Mvvm.ComponentModel;
using Mezon.Application.Interfaces;

namespace Mezon.Presentation.ViewModels
{
    public abstract class ViewModelBase : ObservableObject, IViewModelBase
    {
        public virtual void OnNavigatedTo(object parameter)
        {
        }

        public virtual void OnClosed()
        {
        }
    }
}