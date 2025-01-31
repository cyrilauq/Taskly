namespace Taskly.Natif.Application.Validator.Rules
{
    public interface IValidationRule<T>
    {
        string ValidationMessage { get; init; }

        /// <summary>
        /// Ckeck if the value is not respecting the validation rule
        /// </summary>
        /// <param name="value">Value to check</param>
        /// <returns>
        /// True if the value doesn't match the requirement of the rule, otherwise false
        /// </returns>
        bool Check(T value);
    }
}
