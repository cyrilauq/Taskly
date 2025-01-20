namespace Taskly.Client.Application.Exceptions
{
    public class UnauthorizedAccessException(string message) : ServiceException(message)
    {
    }
}
