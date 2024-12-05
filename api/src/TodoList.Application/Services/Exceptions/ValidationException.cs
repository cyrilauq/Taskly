namespace TodoList.Application.Services.Exceptions
{
    public class ValidationException(string message, string[] validationErrors) : Exception(message)
    {
    }
}
