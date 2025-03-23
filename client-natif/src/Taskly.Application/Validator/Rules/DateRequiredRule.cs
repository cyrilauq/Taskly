namespace Taskly.Natif.Application.Validator.Rules
{
    public class DateRequiredRule : IValidationRule<DateTime>
    {
        public string ValidationMessage { get; init; }

        public bool Check(DateTime value)
        {
            return value == DateTime.MinValue || value == DateTime.Now;
        }
    }
}
