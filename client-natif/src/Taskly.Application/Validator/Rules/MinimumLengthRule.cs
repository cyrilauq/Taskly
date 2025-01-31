namespace Taskly.Natif.Application.Validator.Rules
{
    public class MinimumLengthRule(int minimumLength) : IValidationRule<string>
    {
        public string ValidationMessage { get; init; }

        public bool Check(string value)
        {
            return value == null ? true : value.Length < minimumLength;
        }
    }
}
