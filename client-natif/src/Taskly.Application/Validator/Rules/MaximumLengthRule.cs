namespace Taskly.Natif.Application.Validator.Rules
{
    public class MaximumLengthRule(int maximumLength) : IValidationRule<string>
    {
        public string ValidationMessage { get; init; }

        public bool Check(string value)
        {
            return value == null ? false : value.Length > maximumLength;
        }
    }
}
