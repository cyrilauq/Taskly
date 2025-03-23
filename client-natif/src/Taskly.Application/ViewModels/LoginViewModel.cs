using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services;
using Taskly.Natif.Application.Services.Interface;
using Taskly.Natif.Application.Validator;
using Taskly.Natif.Application.Validator.Rules;

namespace Taskly.Natif.Application.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IAuthenticationService _authenticationService;
        private IStorageService _storageService;
        private IAuthState _authState;
        private IToastService _toastService;
        private INavigationService _navigationService;

        public ValidatableObject<string> UsernameValidator { get; init; }
        public ValidatableObject<string> PasswordValidator { get; init; }

        public bool FormCanBeEdited { get; private set; } = true;

        public LoginViewModel(IAuthenticationService authenticationService, IAuthState authState, IStorageService storageService, IToastService toastService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _authState = authState;
            _storageService = storageService;
            _toastService = toastService;
            _navigationService = navigationService;

            UsernameValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The pseudo is required" } }
            };
            PasswordValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The password is required" } }
            };
            _toastService = toastService;
            _navigationService = navigationService;
        }

        // Notice:
        // This will create a new property of type "IRelayCommand" and of name "LoginCommand"
        // The "RelayCommand" attribute will remove the "On" and "Async" from the base code and add the "Command" at the end of the property name when the property is generated.
        [RelayCommand]
        private async Task OnLoginAsync()
        {
            if (FormIsValid())
            {
                try
                {
                    FormCanBeEdited = false;
                    var result = await _authenticationService.LoginWithCredentials(UsernameValidator.Value as string, PasswordValidator.Value as string);
                    FormCanBeEdited = true;
                    if (result)
                    {
                        await _storageService.SaveAsync(_authState, nameof(IAuthState));
                        await _navigationService.NavigateTo("//Dashboard");
                    }
                }
                catch (ServiceException se)
                {
                    await _toastService.ShowErrorAsync(se.Message);
                }
                catch (Exception ex)
                {
                    if (ex is NotFoundException || ex is ValidationException)
                    {
                        await _toastService.ShowErrorAsync(ex.Message);
                    }
                    else
                    {
                        await _toastService.ShowErrorAsync("An unexpected error occured");
                    }
                }
            }
        }

        private bool FormIsValid()
        {
            var result = false;
            result |= UsernameValidator.Validate();
            result |= PasswordValidator.Validate();
            return !result;
        }
    }
}
