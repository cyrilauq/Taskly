using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Client.Application.State.Interfaces;
using Taskly.Natif.Application.Services.Interface;
using Taskly.Natif.Application.Validator;
using Taskly.Natif.Application.Validator.Rules;

namespace Taskly.Natif.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private IAuthenticationService _authenticationService;
        private IStorageService _storageService;
        private IAuthState _authState;

        public ValidatableObject<string> UsernameValidator { get; init; }
        public ValidatableObject<string> PasswordValidator { get; init; }

        public bool FormCanBeEdited { get; private set; } = true;

        public LoginViewModel(IAuthenticationService authenticationService, IAuthState authState, IStorageService storageService)
        {
            _authenticationService = authenticationService;
            _authState = authState;
            _storageService = storageService;

            UsernameValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The pseudo is required" } }
            };
            PasswordValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The password is required" } }
            };
        }

        // Notice:
        // This will create a new property of type "IRelayCommand" and of name "LoginCommand"
        // The "RelayCommand" attribute will remove the "On" and "Async" from the base code and add the "Command" at the end of the property name when the property is generated.
        [RelayCommand]
        private async Task OnLoginAsync()
        {
            if(FormIsValid())
            {
                try
                {
                    FormCanBeEdited = false;
                    var result = await _authenticationService.LoginWithCredentials(UsernameValidator.Value as string, PasswordValidator.Value as string);
                    FormCanBeEdited = true;
                    if (result)
                    {
                        await _storageService.SaveAsync(_authState, nameof(IAuthState));
                        await Shell.Current.GoToAsync("//Dashboard");
                    }
                }
                catch(ServiceException se)
                {
                    var toast = Toast.Make(se.Message, ToastDuration.Long, 14);
                    await toast.Show();
                }
                catch(Exception ex)
                {
                    if(ex is NotFoundException || ex is ValidationException)
                    {
                        var toast = Toast.Make(ex.Message, ToastDuration.Long, 14);
                        await toast.Show();
                    }
                    else
                    {
                        var toast = Toast.Make("An unexpected error occured", ToastDuration.Long, 14);
                        await toast.Show();
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
