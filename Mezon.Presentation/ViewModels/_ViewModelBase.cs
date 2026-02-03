using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Mezon.Application.Interfaces;

namespace Mezon.Presentation.ViewModels
{
    public abstract class ViewModelBase : ObservableRecipient, IViewModelBase
    {
        protected ViewModelBase() : base(WeakReferenceMessenger.Default)
        {
            
        }
        public virtual void OnNavigatedTo(object parameter)
        {
            IsActive = true;
        }

        public virtual void OnClosed()
        {
            IsActive = false;
        }
    }
}