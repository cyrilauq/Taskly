using System.ComponentModel;
using Maui = CommunityToolkit.Maui.Core;

namespace Taskly.Natif.Services
{
    class NatifPopupService(Maui.IPopupService popupService) : Application.Services.Interface.IPopupService
    {
        public async Task<TReturn?> ShowPopupAsync<TViewModel, TReturn>(Action<TViewModel> onPresentingAction, CancellationToken token = default) where TViewModel : INotifyPropertyChanged
        {
            return (TReturn?)(await popupService.ShowPopupAsync<TViewModel>(onPresentingAction, token));
        }

        public async Task<TReturn?> ShowPopupAsync<TViewModel, TReturn>(CancellationToken token = default) where TViewModel : INotifyPropertyChanged
        {
            return (TReturn?)(await popupService.ShowPopupAsync<TViewModel>(token));
        }
    }
}
