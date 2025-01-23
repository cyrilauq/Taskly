using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services.Interface;

namespace Taskly.Natif.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IAuthenticationService _authenticationService;
        private IStorageService _storageService;
        private IAuthState _authState;

        [ObservableProperty]
        private string? login;
        [ObservableProperty]
        private string? password;

        public bool FormCanBeEdited { get; private set; } = true;

        public LoginViewModel(IAuthenticationService authenticationService, IAuthState authState, IStorageService storageService)
        {
            _authenticationService = authenticationService;
            _authState = authState;
            _storageService = storageService;
        }

        // Notice:
        // This will create a new property of type "IRelayCommand" and of name "LoginCommand"
        // The "RelayCommand" attribute will remove the "On" and "Async" from the base code and add the "Command" at the end of the property name when the property is generated.
        [RelayCommand]
        private async Task OnLoginAsync()
        {
            FormCanBeEdited = false;
            var result = await _authenticationService.LoginWithCredentials(login, password);
            FormCanBeEdited = true;
            if(result)
            {
                await _storageService.SaveAsync(_authState, nameof(IAuthState));
                await Shell.Current.GoToAsync("//Dashboard");
            }
        }
    }
}
