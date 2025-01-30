namespace Taskly.Natif.Application.Validator.Rules
{
    public class MaximumLengthRule(int maximumLength) : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value == null ? false : value.Length > maximumLength;
        }
    }
}
