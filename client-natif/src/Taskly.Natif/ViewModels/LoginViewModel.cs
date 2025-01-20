using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using Taskly.Client.Application.Services.Interfaces;

namespace Taskly.Natif.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IAuthenticationService _authenticationService;
        private string _login;
        private string _password;

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public bool FormCanBeEdited { get; private set; } = true;

        public ICommand OnLoginCommand { private set; get; }

        public LoginViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            OnLoginCommand = new AsyncRelayCommand(OnLogin);
        }

        public async Task<bool> OnLogin()
        {
            FormCanBeEdited = false;
            var result = await _authenticationService.LoginWithCredentials(Login, Password);
            FormCanBeEdited = true;
            return result;
        }
    }
}
