namespace Taskly.Client.Application.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
