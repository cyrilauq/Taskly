using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.Application.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private IAuthState _authState;
        private IStorageService _storageService;
        private INavigationService _navigationService;

        public HomeViewModel(IAuthState authState, IStorageService storageService, INavigationService navigationService)
        {
            _authState = authState;
            _storageService = storageService;
            _navigationService = navigationService;
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
                await _navigationService.NavigateTo("//Dashboard");
            }
        }

        [RelayCommand]
        private void GoToRegister()
        {
            _navigationService.NavigateTo("auth/register");
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
