namespace Taskly.Client.Application.Exceptions
{
    public class InternalServerErrorException(string message) : Exception(message)
    {
    }
}
