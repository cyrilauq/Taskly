namespace Taskly.Natif.Application.Validator.Rules
{
    public class DateRequiredRule : IValidationRule<DateOnly>
    {
        public string ValidationMessage { get; init; }

        public bool Check(DateOnly value)
        {
            return value == DateOnly.MinValue || value == DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
