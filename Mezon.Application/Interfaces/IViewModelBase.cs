namespace Mezon.Application.Interfaces
{
    public interface IViewModelBase
    {
        void OnNavigatedTo(object parameter);

        void OnClosed();
    }
}
