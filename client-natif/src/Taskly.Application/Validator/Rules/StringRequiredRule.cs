namespace Taskly.Natif.Application.Validator.Rules
{
    public class StringRequiredRule : IValidationRule<string>
    {
        public string ValidationMessage { get; init; }

        public bool Check(string value)
        {
            return string.IsNullOrEmpty(value?.Trim());
        }
    }
}
