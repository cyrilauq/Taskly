namespace Taskly.Natif.Application.Validator.Rules
{
    public class MinimumLengthRule(int minimumLength) : IValidationRule<string>
    {
        public string ValidationMessage { get; set; }

        public bool Check(string value)
        {
            return value == null ? true : value.Length < minimumLength;
        }
    }
}
