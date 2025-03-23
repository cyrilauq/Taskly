using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.Application.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {
        private IStorageService _storageService;
        private INavigationService _navigationService;

        [ObservableProperty]
        private string? connectedUser;

        public MenuViewModel(IAuthState authState, IStorageService storageService, INavigationService navigationService)
        {
            _storageService = storageService;
            _navigationService = navigationService;

            authState.OnStateChange += () =>
            {
                ConnectedUser = authState.UserName;
            };
        }

        [RelayCommand]
        private async Task OnLogoutAsync()
        {
            await _storageService.SaveAsync<IAuthState>(null, nameof(IAuthState));
            await _navigationService.NavigateTo("//MainPage");
        }
    }
}
