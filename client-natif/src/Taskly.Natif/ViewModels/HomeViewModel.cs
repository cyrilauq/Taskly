using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.ViewModels
{
    public class HomeViewModel : BindableObject
    {
        private IAuthState _authState;
        private IStorageService _storageService;

        public HomeViewModel(IAuthState authState, IStorageService storageService)
        {
            _authState = authState;
            _storageService = storageService;
        }

        public async Task LoadState()
        {
            var saveState = await _storageService.GetAsync<LocalAuthState>(nameof(IAuthState));
            if (saveState != null)
            {
                _authState.Token = saveState.Token;
                _authState.UserId = saveState.UserId;
                _authState.UserName = saveState.UserName;
                _authState.NotifyStateChanged();
                // Thread safe navigation
                Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(async () => await Shell.Current.GoToAsync("//Dashboard", false));
            }
        }

        private class LocalAuthState : IAuthState
        {
            public bool IsAuthenticated => Token != null;

            public string? UserName { get; set; }
            public Guid? UserId { get; set; }
            public string? Token { get; set; }

            public event IAuthState.StateChangedHandler OnStateChange;

            public void NotifyStateChanged()
            {
                OnStateChange?.Invoke();
            }
        }
    }
}
