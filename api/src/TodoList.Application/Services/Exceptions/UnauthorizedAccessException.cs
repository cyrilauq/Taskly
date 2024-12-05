namespace TodoList.Application.Services.Exceptions
{
    public class UnauthorizedAccessException(string message) : Exception(message);
}
