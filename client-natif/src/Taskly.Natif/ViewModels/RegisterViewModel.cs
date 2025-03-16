using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Taskly.Client.Application.Exceptions;
using Taskly.Client.Application.Model;
using Taskly.Client.Application.Services.Interfaces;
using Taskly.Natif.Application.Services;
using Taskly.Natif.Application.Services.Interface;
using Taskly.Natif.Application.Validator;
using Taskly.Natif.Application.Validator.Rules;

namespace Taskly.Natif.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private IAuthenticationService _authService;
        private IToastService _toastService;
        private ILogger<RegisterViewModel> _logger;

        public ValidatableObject<string> UsernameValidator { get; private set; }
        public ValidatableObject<string> EmailValidator { get; private set; }
        public ValidatableObject<string> FirstnameValidator { get; private set; }
        public ValidatableObject<string> LastnameValidator { get; private set; }
        public ValidatableObject<DateOnly> BirthdateValidator { get; private set; }
        public ValidatableObject<string> PasswordValidator { get; private set; }
        [ObservableProperty]
        private string _error;

        public RegisterViewModel(IAuthenticationService authService, IToastService toastService, ILogger<RegisterViewModel> logger, INavigationService navigationService)
        {
            InitValidators();

            _authService = authService;
            _toastService = toastService;
            _logger = logger;
        }

        private void InitValidators()
        {
            UsernameValidator = new()
            {
                Rules = new() 
                { 
                    new StringRequiredRule { ValidationMessage = "The pseudo is required" },
                    new MaximumLengthRule(25) { ValidationMessage = "The pseudo should'nt be longer than 25 characters" },
                    new MinimumLengthRule(4) { ValidationMessage = "The pseudo should'nt be shorter than 4 characters" }
                }
            };
            PasswordValidator = new()
            {
                Rules = new() 
                {
                    new StringRequiredRule { ValidationMessage = "The password is required" },
                    new MaximumLengthRule(25) { ValidationMessage = "The password should'nt be longer than 25 characters" },
                    new MinimumLengthRule(8) { ValidationMessage = "The password should'nt be shorter than 8 characters" }
                }
            };
            EmailValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The email is required" } }
            };
            FirstnameValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The firstname is required" } }
            };
            LastnameValidator = new()
            {
                Rules = new() { new StringRequiredRule { ValidationMessage = "The lastname is required" } }
            };
            BirthdateValidator = new()
            {
                Rules = new() { new DateRequiredRule { ValidationMessage = "The birthdate is required" } }
            };
        }

        [RelayCommand]
        private async Task Register()
        {
            try
            {
                Error = null;
                if (ValidatorsHasErrors())
                {
                    Error = "One or more field have errors";
                    return;
                }
                if(await _authService.RegisterUser(ComputeModelFromValidators()))
                {
                    await _toastService.ShowMessageAsync("You've successfully registered");
                }
            }
            catch(ServiceException se)
            {
                _logger.LogTrace(se, se.Message);
                await _toastService.ShowErrorAsync(se.Message);
            }
            catch(Exception ex)
            {
                _logger.LogTrace(ex, ex.Message);
                await _toastService.ShowErrorAsync("An unexpected error occured");
            }
        }

        private RegisterModel ComputeModelFromValidators()
        {
            RegisterModel model = new RegisterModel();
            model.BirthDate = (DateOnly?)BirthdateValidator.Value ?? DateOnly.MinValue;
            model.ConfirmPassword = (string?)PasswordValidator.Value ?? "";
            model.Password = (string?)PasswordValidator.Value ?? "";
            model.Pseudo = (string?)UsernameValidator.Value ?? "";
            model.Firstname = (string?)FirstnameValidator.Value ?? "";
            model.Lastname = (string?)LastnameValidator.Value ?? "";
            model.Email = (string?)EmailValidator.Value ?? "";
            return model;
        }

        private bool ValidatorsHasErrors()
        {
            BirthdateValidator.Validate();
            PasswordValidator.Validate();
            UsernameValidator.Validate();
            FirstnameValidator.Validate();
            LastnameValidator.Validate();
            EmailValidator.Validate();
            return BirthdateValidator.HasError
                || PasswordValidator.HasError
                || UsernameValidator.HasError
                || FirstnameValidator.HasError
                || LastnameValidator.HasError
                || EmailValidator.HasError;
        }
    }
}
