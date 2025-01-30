using CommunityToolkit.Mvvm.ComponentModel;
using Taskly.Natif.Application.Validator.Rules;

namespace Taskly.Natif.Application.Validator
{
    public class ValidatableObject<T> : ObservableObject, IValidatorObject
    {
        private bool _hasError;
        private object _value;
        private string _error;
        public bool HasError
        {
            get => _hasError;
            private set => SetProperty(ref _hasError, value);
        }

        public List<IValidationRule<T>> Rules { get; set; } = new();
        public string? Error
        {
            get => _error;
            private set => SetProperty(ref _error, value);
        }
        public object? Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }

        public bool Validate()
        {
            var errors = Rules.Where(r => r.Check((T)Value)).ToList();
            Error = errors.Any() ? errors[0].ValidationMessage : null;
            HasError = errors.Any();
            return errors.Any();
        }
    }
}
