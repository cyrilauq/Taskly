namespace Taskly.Natif.Application.Validator
{
    public interface IValidatorObject
    {
        bool HasError { get; }
        string? Error { get; }
        object? Value { get; set; }

        bool Validate();
    }
}
