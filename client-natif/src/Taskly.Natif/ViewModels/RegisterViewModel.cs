using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Taskly.Client.Application.Exceptions;
using Taskly.Natif.Application.Validator;

namespace Taskly.Natif.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        public ValidatableObject<string> UsernameValidator { get; init; }
        public ValidatableObject<string> EmailValidator { get; init; }
        public ValidatableObject<string> FirstnameValidator { get; init; }
        public ValidatableObject<string> LastnameValidator { get; init; }
        public ValidatableObject<DateOnly> BirthdateValidator { get; init; }
        public ValidatableObject<string> PasswordValidator { get; init; }

        public RegisterViewModel()
        {
            UsernameValidator = new();
            PasswordValidator = new();
            EmailValidator = new();
            FirstnameValidator = new();
            LastnameValidator = new();
            BirthdateValidator = new();
        }

        [RelayCommand]
        private async Task Register()
        {
            try
            {

            }
            catch(ServiceException se)
            {

            }
            catch(Exception ex)
            {

            }
        }
    }
}
