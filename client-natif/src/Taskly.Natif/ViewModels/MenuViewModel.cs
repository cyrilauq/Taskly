using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.ViewModels
{
    public partial class MenuViewModel : ObservableObject
    {
        private IStorageService _storageService;

        [ObservableProperty]
        private string? connectedUser;

        public MenuViewModel(IAuthState authState, IStorageService storageService)
        {
            _storageService = storageService;

            authState.OnStateChange += () =>
            {
                ConnectedUser = authState.UserName;
            };
        }

        [RelayCommand]
        private async Task OnLogoutAsync()
        {
            await _storageService.SaveAsync<IAuthState>(null, nameof(IAuthState));
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
