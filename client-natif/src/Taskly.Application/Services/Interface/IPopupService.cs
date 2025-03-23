using System.ComponentModel;

namespace Taskly.Natif.Application.Services.Interface
{
    public interface IPopupService
    {
        Task<TReturn?> ShowPopupAsync<TViewModel, TReturn>(Action<TViewModel> onPresentingAction, CancellationToken token = default) where TViewModel : INotifyPropertyChanged;
        Task<TReturn?> ShowPopupAsync<TViewModel, TReturn>(CancellationToken token = default) where TViewModel : INotifyPropertyChanged;
    }
}
